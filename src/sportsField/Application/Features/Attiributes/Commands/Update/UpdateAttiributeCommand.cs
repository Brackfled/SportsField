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

namespace Application.Features.Attiributes.Commands.Update;

public class UpdateAttiributeCommand : IRequest<UpdatedAttiributeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public string[] Roles => [Admin, Write, AttiributesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttiributes"];

    public class UpdateAttiributeCommandHandler : IRequestHandler<UpdateAttiributeCommand, UpdatedAttiributeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttiributeRepository _attiributeRepository;
        private readonly AttiributeBusinessRules _attiributeBusinessRules;

        public UpdateAttiributeCommandHandler(IMapper mapper, IAttiributeRepository attiributeRepository,
                                         AttiributeBusinessRules attiributeBusinessRules)
        {
            _mapper = mapper;
            _attiributeRepository = attiributeRepository;
            _attiributeBusinessRules = attiributeBusinessRules;
        }

        public async Task<UpdatedAttiributeResponse> Handle(UpdateAttiributeCommand request, CancellationToken cancellationToken)
        {
            Attiribute? attiribute = await _attiributeRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _attiributeBusinessRules.AttiributeShouldExistWhenSelected(attiribute);
            attiribute = _mapper.Map(request, attiribute);

            await _attiributeRepository.UpdateAsync(attiribute!);

            UpdatedAttiributeResponse response = _mapper.Map<UpdatedAttiributeResponse>(attiribute);
            return response;
        }
    }
}