using Amazon.Runtime.Internal;
using Application.Features.Courts.Constants;
using Application.Services.CourtReservations;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListCeoItems;
public class GetListCeoItemsQuery: IRequest<GetListCeoItemsDto>, ITransactionalRequest, ISecuredRequest, ICachableRequest
{
    public string[] Roles => [CourtsOperationClaims.CeoItemsRead];

    public bool BypassCache { get; }

    public string CacheKey => "GetCeoItem";

    public string? CacheGroupKey => "GetCeoItems";

    public TimeSpan? SlidingExpiration => TimeSpan.FromDays(0.5);

    public class GetListCeoItemsQueryHandler: IRequestHandler<GetListCeoItemsQuery, GetListCeoItemsDto>
    {
        private readonly IUserService _userService;
        private readonly ICourtReservationService _courtReservationService;
        private readonly ICourtRepository _courtRepository;

        public GetListCeoItemsQueryHandler(IUserService userService, ICourtReservationService courtReservationService, ICourtRepository courtRepository)
        {
            _userService = userService;
            _courtReservationService = courtReservationService;
            _courtRepository = courtRepository;
        }

        public async Task<GetListCeoItemsDto> Handle(GetListCeoItemsQuery request, CancellationToken cancellationToken)
        {
            GetListCeoItemsDto response = new();

            ICollection<User>? users = await _userService.GetAllAsync();

            int courtOwnerCounter = 0;
            foreach (User user in users)
            {
                if (user.UserState == UserState.CourtOwner)
                    courtOwnerCounter++;
            }

            response.TotalUserCount = users.Count;
            response.TotalCourtOwnerCount = courtOwnerCounter;

            ICollection<Court> courts = await _courtRepository.GetAllAsync();

            int activeCourtCounter = 0;
            foreach (Court court in courts)
            {
                if(court.IsActive == true)
                    activeCourtCounter++;
            }

            response.TotalCourtsCount = courts.Count;
            response.ActiveCourtsCount = activeCourtCounter;

            ICollection<CourtReservation> courtReservations = await _courtReservationService.GetAllAsync();

            int reservedReservationCounter = 0;
            foreach(CourtReservation courtReservation in courtReservations)
            {
                if(courtReservation.UserId != null && courtReservation.IsActive == true)
                    reservedReservationCounter++;
            }

            response.TotalReservationCount = courtReservations.Count;
            response.ReservedReservationCount = reservedReservationCounter;

            return response;
        }
    }
}
