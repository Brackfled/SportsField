using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Queries.GetListByDynamic;
public class GetListByDynamicCourtReservationQuery: IRequest<GetListResponse<GetListByDynamicCourtReservationListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicCourtReservationQueryHandler: IRequestHandler<GetListByDynamicCourtReservationQuery, GetListResponse<GetListByDynamicCourtReservationListItemDto>>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private IMapper _mapper;

        public GetListByDynamicCourtReservationQueryHandler(ICourtReservationRepository courtReservationRepository, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicCourtReservationListItemDto>> Handle(GetListByDynamicCourtReservationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CourtReservation>? courtReservations = await _courtReservationRepository.GetListByDynamicAsync(
                    dynamic: request.DynamicQuery,
                    size:request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken:cancellationToken
                );

            GetListResponse<GetListByDynamicCourtReservationListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicCourtReservationListItemDto>>(courtReservations);
            return response;
        }
    }
}
