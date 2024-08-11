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

namespace Application.Features.CourtReservations.Commands.Create;

public class CreateCourtReservationCommand : IRequest<CreatedCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required Guid CourtId { get; set; }
    public Guid? UserId { get; set; }
    public required DateTime AvailableDate { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required long CreatedTime { get; set; }
    public required bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, CourtReservationsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class CreateCourtReservationCommandHandler : IRequestHandler<CreateCourtReservationCommand, CreatedCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

        public CreateCourtReservationCommandHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository,
                                         CourtReservationBusinessRules courtReservationBusinessRules)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
        }

        public async Task<CreatedCourtReservationResponse> Handle(CreateCourtReservationCommand request, CancellationToken cancellationToken)
        {
            CourtReservation courtReservation = _mapper.Map<CourtReservation>(request);

            await _courtReservationRepository.AddAsync(courtReservation);

            CreatedCourtReservationResponse response = _mapper.Map<CreatedCourtReservationResponse>(courtReservation);
            return response;
        }
    }
}