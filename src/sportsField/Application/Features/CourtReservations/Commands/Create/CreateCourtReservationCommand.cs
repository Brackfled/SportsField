using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CourtReservations.Constants.CourtReservationsOperationClaims;
using Domain.Dtos;
using Application.Features.Courts.Rules;
using Application.Services.Courts;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreateCourtReservationCommand : IRequest<CreatedCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public CreateCourtReservationCommandDto CreateCourtReservationCommandDto { get; set; }

    public string[] Roles => [Admin, CourtReservationsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class CreateCourtReservationCommandHandler : IRequestHandler<CreateCourtReservationCommand, CreatedCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly ICourtService _courtService;

        public CreateCourtReservationCommandHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, ICourtService courtService)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _courtService = courtService;
        }

        public async Task<CreatedCourtReservationResponse> Handle(CreateCourtReservationCommand request, CancellationToken cancellationToken)
        {

            (IList<ReservationDetailDto> saveTimes, IList<ReservationDetailDto> unsaveTimes) = await _courtReservationBusinessRules.ReservationsTimeControl(request.CreateCourtReservationCommandDto.ReservationDetailDtos);

            IList<Guid> courtIds = new List<Guid>();

            foreach (Guid courtId in request.CreateCourtReservationCommandDto.CourtIds)
            {
                Court? court = await _courtService.GetAsync(c =>c.Id == courtId);
                await _courtBusinessRules.CourtShouldExistWhenSelected(court);
                await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, Admin);
                await _courtReservationBusinessRules.CourtShouldBeAvailableWeek(court);

                foreach (DateTime dateTime in request.CreateCourtReservationCommandDto.ReservationDates)
                {
                    await _courtReservationBusinessRules.CourtReservationDateShouldBeMatchedDateNow(dateTime);

                    foreach (ReservationDetailDto time in saveTimes)
                    {
                        string[] times = time.Times.Split('-');

                        CourtReservation courtReservation = new()
                        {
                            Id = Guid.NewGuid(),
                            UserId = null,
                            AvailableDate = dateTime.Date,
                            CourtId = courtId,
                            CreatedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                            StartTime = TimeSpan.Parse(times[0]),
                            EndTime = TimeSpan.Parse(times[1]),
                            IsActive = true,
                            Price = time.Price,
                        };

                        await _courtReservationRepository.AddAsync(courtReservation);
                        courtIds.Add(court!.Id);
                    }
                }
            }

            CreatedCourtReservationResponse response = new() { CourtIds = courtIds, SavedTimes = saveTimes, UnsavedTimes = unsaveTimes };
            return response;
        }
    }
}