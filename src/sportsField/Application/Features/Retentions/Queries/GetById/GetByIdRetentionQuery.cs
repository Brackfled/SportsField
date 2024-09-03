using Application.Features.Retentions.Constants;
using Application.Features.Retentions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Retentions.Constants.RetentionsOperationClaims;

namespace Application.Features.Retentions.Queries.GetById;

public class GetByIdRetentionQuery : IRequest<GetByIdRetentionResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdRetentionQueryHandler : IRequestHandler<GetByIdRetentionQuery, GetByIdRetentionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRetentionRepository _retentionRepository;
        private readonly RetentionBusinessRules _retentionBusinessRules;

        public GetByIdRetentionQueryHandler(IMapper mapper, IRetentionRepository retentionRepository, RetentionBusinessRules retentionBusinessRules)
        {
            _mapper = mapper;
            _retentionRepository = retentionRepository;
            _retentionBusinessRules = retentionBusinessRules;
        }

        public async Task<GetByIdRetentionResponse> Handle(GetByIdRetentionQuery request, CancellationToken cancellationToken)
        {
            Retention? retention = await _retentionRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _retentionBusinessRules.RetentionShouldExistWhenSelected(retention);

            GetByIdRetentionResponse response = _mapper.Map<GetByIdRetentionResponse>(retention);
            return response;
        }
    }
}