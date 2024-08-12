using Amazon.Runtime.Internal;
using Application.Features.Courts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListByUserId;
public class GetListByUserIdCourtQuery: IRequest<GetListResponse<GetListByUserIdCourtListItemDto>>, ISecuredRequest, ICachableRequest
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [CourtsOperationClaims.Admin, CourtsOperationClaims.Read];

    public bool BypassCache {  get; set; }

    public string CacheKey => $"GetListByUserId({PageRequest.PageSize},{PageRequest.PageIndex},UserId:{UserId})";

    public string? CacheGroupKey => "GetCourts";

    public TimeSpan? SlidingExpiration {  get; set; }

    public class GetListByUserIdCourtQueryHandler : IRequestHandler<GetListByUserIdCourtQuery, GetListResponse<GetListByUserIdCourtListItemDto>>
    {
        private readonly ICourtRepository _courtRepository;
        private IMapper _mapper;

        public GetListByUserIdCourtQueryHandler(ICourtRepository courtRepository, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByUserIdCourtListItemDto>> Handle(GetListByUserIdCourtQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Court>? courts = await _courtRepository.GetListAsync(
                    predicate: c=> c.UserId == request.UserId,
                    include: c => c.Include(opt => opt.Attiributes!).Include(opt => opt.CourtImages!),
                    size:request.PageRequest.PageSize,
                    index:request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByUserIdCourtListItemDto> response = _mapper.Map<GetListResponse<GetListByUserIdCourtListItemDto>>(courts);
            return response;
        }
    }
}
