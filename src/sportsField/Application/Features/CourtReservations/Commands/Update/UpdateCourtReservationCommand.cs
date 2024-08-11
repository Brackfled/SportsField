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

namespace Application.Features.CourtReservations.Commands.Update;

public class UpdateCourtReservationCommand : IRequest<UpdatedCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid CourtId { get; set; }
    public Guid? UserId { get; set; }
    public required DateTime AvailableDate { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required long CreatedTime { get; set; }
    public required bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, CourtReservationsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class UpdateCourtReservationCommandHandler : IRequestHandler<UpdateCourtReservationCommand, UpdatedCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

        public UpdateCourtReservationCommandHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository,
                                         CourtReservationBusinessRules courtReservationBusinessRules)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
        }

        public async Task<UpdatedCourtReservationResponse> Handle(UpdateCourtReservationCommand request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(predicate: cr => cr.Id == request.Id, cancellationToken: cancellationToken);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);
            courtReservation = _mapper.Map(request, courtReservation);

            await _courtReservationRepository.UpdateAsync(courtReservation!);

            UpdatedCourtReservationResponse response = _mapper.Map<UpdatedCourtReservationResponse>(courtReservation);
            return response;
        }
    }
}