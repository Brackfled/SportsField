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

namespace Application.Features.CourtReservations.Commands.Delete;

public class DeleteCourtReservationCommand : IRequest<DeletedCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CourtReservationsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class DeleteCourtReservationCommandHandler : IRequestHandler<DeleteCourtReservationCommand, DeletedCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

        public DeleteCourtReservationCommandHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository,
                                         CourtReservationBusinessRules courtReservationBusinessRules)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
        }

        public async Task<DeletedCourtReservationResponse> Handle(DeleteCourtReservationCommand request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(predicate: cr => cr.Id == request.Id, cancellationToken: cancellationToken);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

            await _courtReservationRepository.DeleteAsync(courtReservation!);

            DeletedCourtReservationResponse response = _mapper.Map<DeletedCourtReservationResponse>(courtReservation);
            return response;
        }
    }
}