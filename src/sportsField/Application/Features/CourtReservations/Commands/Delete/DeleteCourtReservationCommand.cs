using Application.Features.CourtReservations.Constants;
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
using Application.Features.Courts.Rules;

namespace Application.Features.CourtReservations.Commands.Delete;

public class DeleteCourtReservationCommand : IRequest<DeletedCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    public string[] Roles => [Admin, CourtReservationsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class DeleteCourtReservationCommandHandler : IRequestHandler<DeleteCourtReservationCommand, DeletedCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

        public DeleteCourtReservationCommandHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository, CourtBusinessRules courtBusinessRules, CourtReservationBusinessRules courtReservationBusinessRules)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtBusinessRules = courtBusinessRules;
            _courtReservationBusinessRules = courtReservationBusinessRules;
        }

        public async Task<DeletedCourtReservationResponse> Handle(DeleteCourtReservationCommand request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(predicate: cr => cr.Id == request.Id, cancellationToken: cancellationToken);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

            await _courtBusinessRules.UserIdNotMatchedCourtUserId(courtReservation!.CourtId, request.UserId, Admin);

            await _courtReservationRepository.DeleteAsync(courtReservation!, true);

            DeletedCourtReservationResponse response = _mapper.Map<DeletedCourtReservationResponse>(courtReservation);
            return response;
        }
    }
}