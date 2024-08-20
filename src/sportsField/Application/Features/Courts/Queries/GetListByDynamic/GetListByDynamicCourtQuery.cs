using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListByDynamic;
public class GetListByDynamicCourtQuery: IRequest<GetListResponse<GetListByDynamicCourtListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicCourtQueryHandler: IRequestHandler<GetListByDynamicCourtQuery, GetListResponse<GetListByDynamicCourtListItemDto>>
    {
        private readonly ICourtRepository _courtRepository;
        private IMapper _mapper;

        public GetListByDynamicCourtQueryHandler(ICourtRepository courtRepository, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicCourtListItemDto>> Handle(GetListByDynamicCourtQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Court>? courts = await _courtRepository.GetListByDynamicAsync(
                    include: c => c.Include(opt => opt.Attiributes!).Include(opt => opt.CourtImages!),
                    dynamic: request.DynamicQuery,
                    size: request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByDynamicCourtListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicCourtListItemDto>>(courts);
            return response;
        }
    }
}
