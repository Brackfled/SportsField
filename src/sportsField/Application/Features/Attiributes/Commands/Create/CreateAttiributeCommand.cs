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

namespace Application.Features.Attiributes.Commands.Create;

public class CreateAttiributeCommand : IRequest<CreatedAttiributeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required string Name { get; set; }

    public string[] Roles => [Admin, Write, AttiributesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAttiributes"];

    public class CreateAttiributeCommandHandler : IRequestHandler<CreateAttiributeCommand, CreatedAttiributeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttiributeRepository _attiributeRepository;
        private readonly AttiributeBusinessRules _attiributeBusinessRules;

        public CreateAttiributeCommandHandler(IMapper mapper, IAttiributeRepository attiributeRepository,
                                         AttiributeBusinessRules attiributeBusinessRules)
        {
            _mapper = mapper;
            _attiributeRepository = attiributeRepository;
            _attiributeBusinessRules = attiributeBusinessRules;
        }

        public async Task<CreatedAttiributeResponse> Handle(CreateAttiributeCommand request, CancellationToken cancellationToken)
        {
            Attiribute attiribute = _mapper.Map<Attiribute>(request);

            await _attiributeRepository.AddAsync(attiribute);

            CreatedAttiributeResponse response = _mapper.Map<CreatedAttiributeResponse>(attiribute);
            return response;
        }
    }
}