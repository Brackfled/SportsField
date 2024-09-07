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

namespace Application.Features.Attiributes.Queries.GetListByDynamic;
public class GetListByDynamicAttiributeQuery : IRequest<GetListResponse<GetListByDynamicAttiributeListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicAttiributeQueryHandler : IRequestHandler<GetListByDynamicAttiributeQuery, GetListResponse<GetListByDynamicAttiributeListItemDto>>
    {
        private readonly IAttiributeRepository _attiributeRepository;
        private IMapper _mapper;

        public GetListByDynamicAttiributeQueryHandler(IAttiributeRepository attiributeRepository, IMapper mapper)
        {
            _attiributeRepository = attiributeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicAttiributeListItemDto>> Handle(GetListByDynamicAttiributeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Attiribute> attiributes = await _attiributeRepository.GetListByDynamicAsync(
                    dynamic: request.DynamicQuery,
                    include: a => a.Include(opt => opt.Courts!),
                    size: request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByDynamicAttiributeListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicAttiributeListItemDto>>(attiributes);
            return response;
        }
    }
}