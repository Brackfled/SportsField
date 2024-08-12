using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
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

namespace Application.Features.CourtReservations.Queries.GetListByUserId;
public class GetListByUserIdCourtReservationQuery: IRequest<GetListResponse<GetListByUserIdCourtReservationListItemDto>>
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; }


    public class GetListByUserIdCourtReservationQueryHandler: IRequestHandler<GetListByUserIdCourtReservationQuery, GetListResponse<GetListByUserIdCourtReservationListItemDto>>
    {
        private readonly IUserService _userService;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private IMapper _mapper;

        public GetListByUserIdCourtReservationQueryHandler(IUserService userService, ICourtReservationRepository courtReservationRepository, UserBusinessRules userBusinessRules, CourtReservationBusinessRules courtReservationBusinessRules, IMapper mapper)
        {
            _userService = userService;
            _courtReservationRepository = courtReservationRepository;
            _userBusinessRules = userBusinessRules;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByUserIdCourtReservationListItemDto>> Handle(GetListByUserIdCourtReservationQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            IPaginate<CourtReservation>? courtReservation = await _courtReservationRepository.GetListAsync(
                    predicate: cr => cr.UserId == user!.Id,
                    include: cr => cr.Include(opt => opt.Court!),
                    size:request.PageRequest.PageSize,
                    index: request.PageRequest.PageIndex,
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListByUserIdCourtReservationListItemDto> response = _mapper.Map<GetListResponse<GetListByUserIdCourtReservationListItemDto>>(courtReservation);
            return response;
        }
    }
}
