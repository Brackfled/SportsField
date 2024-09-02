using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Queries.GetListByCourtUserId;
public class GetListByCourtUserIdCourtReservationQuery: IRequest<GetListResponse<GetListByCourtUserIdCourtReservationListItemDto>>
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListByCourtUserIdCourtReservationQueryHandler: IRequestHandler<GetListByCourtUserIdCourtReservationQuery, GetListResponse<GetListByCourtUserIdCourtReservationListItemDto>>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly ICourtService _courtService;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public GetListByCourtUserIdCourtReservationQueryHandler(ICourtReservationRepository courtReservationRepository, ICourtService courtService, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _courtService = courtService;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByCourtUserIdCourtReservationListItemDto>> Handle(GetListByCourtUserIdCourtReservationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CourtReservation> courtReservations = await _courtReservationRepository.GetListAsync(
                    predicate: cr => cr.Court!.UserId == request.UserId && cr.UserId != null,
                    include: cr => cr.Include(opt => opt.User!).Include(opt =>opt.Court!),
                    size:request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByCourtUserIdCourtReservationListItemDto> response = _mapper.Map<GetListResponse<GetListByCourtUserIdCourtReservationListItemDto>>(courtReservations);
            return response;
        }
    }
}
