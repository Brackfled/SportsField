using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListByIsActive;
public class GetListByIsActiveCourtQuery: IRequest<GetListResponse<GetListByIsActiveCourtListItemDto>>, ICachableRequest
{
    public bool IsActive { get; set; }
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }

    public string CacheKey => $"GetListByIsActive({PageRequest.PageIndex},{PageRequest.PageSize},{IsActive})";

    public string? CacheGroupKey => "GetCourts";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListByIsActiveCourtQueryHandler: IRequestHandler<GetListByIsActiveCourtQuery, GetListResponse<GetListByIsActiveCourtListItemDto>>
    {
        private readonly ICourtRepository _courtRepository;
        private IMapper _mapper;

        public GetListByIsActiveCourtQueryHandler(ICourtRepository courtRepository, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByIsActiveCourtListItemDto>> Handle(GetListByIsActiveCourtQuery request, CancellationToken cancellationToken)
        {
            bool isActive = false;

            if(request.IsActive)
                isActive = true;

            IPaginate<Court> courts = await _courtRepository.GetListAsync(
                    predicate: c => c.IsActive == isActive,
                    include: c => c.Include(opt => opt.CourtImages!).Include(opt => opt.Attiributes!),
                    size: request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByIsActiveCourtListItemDto> response = _mapper.Map<GetListResponse<GetListByIsActiveCourtListItemDto>>(courts);
            return response;
        }
    }
}
