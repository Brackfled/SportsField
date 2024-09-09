using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Features.Suspends.Constants;
using Application.Features.Suspends.Rules;
using Application.Services.CourtReservations;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;

namespace Application.Features.Suspends.Commands.Create;

public class CreateSuspendCommand : IRequest<CreatedSuspendResponse>, ITransactionalRequest, ISecuredRequest
{
    public Guid UserId { get; set; }
    public CreateSuspendCommandDto CreateSuspendCommandDto { get; set; }

    public string[] Roles => [SuspendsOperationClaims.Admin, SuspendsOperationClaims.Create];

    public class CreateSuspendCommandHandler : IRequestHandler<CreateSuspendCommand, CreatedSuspendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISuspendRepository _suspendRepository;
        private readonly ICourtReservationService _courtReservationService;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly SuspendBusinessRules _suspendBusinessRules;

        public CreateSuspendCommandHandler(IMapper mapper, ISuspendRepository suspendRepository, ICourtReservationService courtReservationService, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, SuspendBusinessRules suspendBusinessRules)
        {
            _mapper = mapper;
            _suspendRepository = suspendRepository;
            _courtReservationService = courtReservationService;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _suspendBusinessRules = suspendBusinessRules;
        }

        public async Task<CreatedSuspendResponse> Handle(CreateSuspendCommand request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationService.GetAsync(cr => cr.Id == request.CreateSuspendCommandDto.CourtReservationId);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation!);
            await _courtReservationBusinessRules.CourtReservationShouldBeRented(courtReservation!);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(courtReservation!.CourtId, request.UserId, SuspendsOperationClaims.Admin);

            if (courtReservation.AvailableDate >= DateTime.UtcNow)
                throw new BusinessException(SuspendsBusinessMessages.ReservationDateIsNotPass);


            Suspend suspend = new()
            {
                Id = Guid.NewGuid(),
                UserId = courtReservation.UserId,
                SuspensionPeriod = DateTime.UtcNow.AddDays(5),
                Reason = $"{courtReservation.AvailableDate} tarihli; {courtReservation.StartTime}-{courtReservation.EndTime} zamanlý randevunuza gitmediðiniz için hesabýnýz bir süreliðine askýya alýndý."
            };

            Suspend addedSuspend = await _suspendRepository.AddAsync(suspend);
            CreatedSuspendResponse response = _mapper.Map<CreatedSuspendResponse>(suspend);
            return response;
        }
    }
}