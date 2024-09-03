using Application.Features.Retentions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Retentions.Constants.RetentionsOperationClaims;

namespace Application.Features.Retentions.Queries.GetList;

public class GetListRetentionQuery : IRequest<GetListResponse<GetListRetentionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListRetentionQueryHandler : IRequestHandler<GetListRetentionQuery, GetListResponse<GetListRetentionListItemDto>>
    {
        private readonly IRetentionRepository _retentionRepository;
        private readonly IMapper _mapper;

        public GetListRetentionQueryHandler(IRetentionRepository retentionRepository, IMapper mapper)
        {
            _retentionRepository = retentionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRetentionListItemDto>> Handle(GetListRetentionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Retention> retentions = await _retentionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListRetentionListItemDto> response = _mapper.Map<GetListResponse<GetListRetentionListItemDto>>(retentions);
            return response;
        }
    }
}