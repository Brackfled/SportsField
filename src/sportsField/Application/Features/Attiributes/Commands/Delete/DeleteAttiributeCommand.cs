using Application.Features.Attiributes.Constants;
using Application.Features.Attiributes.Constants;
using Application.Features.Attiributes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Attiributes.Constants.AttiributesOperationClaims;

namespace Application.Features.Attiributes.Commands.Delete;

public class DeleteAttiributeCommand : IRequest<DeletedAttiributeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, AttiributesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttiributes"];

    public class DeleteAttiributeCommandHandler : IRequestHandler<DeleteAttiributeCommand, DeletedAttiributeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttiributeRepository _attiributeRepository;
        private readonly AttiributeBusinessRules _attiributeBusinessRules;

        public DeleteAttiributeCommandHandler(IMapper mapper, IAttiributeRepository attiributeRepository,
                                         AttiributeBusinessRules attiributeBusinessRules)
        {
            _mapper = mapper;
            _attiributeRepository = attiributeRepository;
            _attiributeBusinessRules = attiributeBusinessRules;
        }

        public async Task<DeletedAttiributeResponse> Handle(DeleteAttiributeCommand request, CancellationToken cancellationToken)
        {
            Attiribute? attiribute = await _attiributeRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _attiributeBusinessRules.AttiributeShouldExistWhenSelected(attiribute);

            await _attiributeRepository.DeleteAsync(attiribute!);

            DeletedAttiributeResponse response = _mapper.Map<DeletedAttiributeResponse>(attiribute);
            return response;
        }
    }
}