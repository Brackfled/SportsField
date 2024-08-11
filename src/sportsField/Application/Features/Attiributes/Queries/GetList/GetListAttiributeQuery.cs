using Application.Features.Attiributes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Attiributes.Constants.AttiributesOperationClaims;

namespace Application.Features.Attiributes.Queries.GetList;

public class GetListAttiributeQuery : IRequest<GetListResponse<GetListAttiributeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAttiributes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAttiributes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAttiributeQueryHandler : IRequestHandler<GetListAttiributeQuery, GetListResponse<GetListAttiributeListItemDto>>
    {
        private readonly IAttiributeRepository _attiributeRepository;
        private readonly IMapper _mapper;

        public GetListAttiributeQueryHandler(IAttiributeRepository attiributeRepository, IMapper mapper)
        {
            _attiributeRepository = attiributeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAttiributeListItemDto>> Handle(GetListAttiributeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Attiribute> attiributes = await _attiributeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAttiributeListItemDto> response = _mapper.Map<GetListResponse<GetListAttiributeListItemDto>>(attiributes);
            return response;
        }
    }
}