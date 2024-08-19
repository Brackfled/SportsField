using Application.Features.Courts.Constants;
using Application.Features.Courts.Constants;
using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Courts.Constants.CourtsOperationClaims;

namespace Application.Features.Courts.Commands.Delete;

public class DeleteCourtCommand : IRequest<DeletedCourtResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    public string[] Roles => [Admin, CourtsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourts"];

    public class DeleteCourtCommandHandler : IRequestHandler<DeleteCourtCommand, DeletedCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly ICourtReservationRepository _courtReservationRepository;

        public DeleteCourtCommandHandler(IMapper mapper, ICourtRepository courtRepository, CourtBusinessRules courtBusinessRules, ICourtReservationRepository courtReservationRepository)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
            _courtReservationRepository = courtReservationRepository;
        }

        public async Task<DeletedCourtResponse> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);

            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, Admin);

            ICollection<CourtReservation>? courtReservations = await _courtReservationRepository.GetAllAsync(cr => cr.CourtId == court!.Id);
            await _courtReservationRepository.DeleteRangeAsync(courtReservations, true);

            await _courtRepository.DeleteAsync(court!, true);

            DeletedCourtResponse response = _mapper.Map<DeletedCourtResponse>(court);
            return response;
        }
    }
}