using Application.Features.Courts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Courts.Constants.CourtsOperationClaims;

namespace Application.Features.Courts.Queries.GetList;

public class GetListCourtQuery : IRequest<GetListResponse<GetListCourtListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCourts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCourts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCourtQueryHandler : IRequestHandler<GetListCourtQuery, GetListResponse<GetListCourtListItemDto>>
    {
        private readonly ICourtRepository _courtRepository;
        private readonly IMapper _mapper;

        public GetListCourtQueryHandler(ICourtRepository courtRepository, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCourtListItemDto>> Handle(GetListCourtQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Court> courts = await _courtRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCourtListItemDto> response = _mapper.Map<GetListResponse<GetListCourtListItemDto>>(courts);
            return response;
        }
    }
}