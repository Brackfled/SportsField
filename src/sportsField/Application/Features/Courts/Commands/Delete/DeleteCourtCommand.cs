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
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CourtsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourts"];

    public class DeleteCourtCommandHandler : IRequestHandler<DeleteCourtCommand, DeletedCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;

        public DeleteCourtCommandHandler(IMapper mapper, ICourtRepository courtRepository,
                                         CourtBusinessRules courtBusinessRules)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<DeletedCourtResponse> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);

            await _courtRepository.DeleteAsync(court!);

            DeletedCourtResponse response = _mapper.Map<DeletedCourtResponse>(court);
            return response;
        }
    }
}