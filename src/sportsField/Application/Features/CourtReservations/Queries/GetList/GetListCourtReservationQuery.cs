using Application.Features.CourtReservations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.CourtReservations.Constants.CourtReservationsOperationClaims;

namespace Application.Features.CourtReservations.Queries.GetList;

public class GetListCourtReservationQuery : IRequest<GetListResponse<GetListCourtReservationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCourtReservations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCourtReservations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCourtReservationQueryHandler : IRequestHandler<GetListCourtReservationQuery, GetListResponse<GetListCourtReservationListItemDto>>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly IMapper _mapper;

        public GetListCourtReservationQueryHandler(ICourtReservationRepository courtReservationRepository, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCourtReservationListItemDto>> Handle(GetListCourtReservationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CourtReservation> courtReservations = await _courtReservationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCourtReservationListItemDto> response = _mapper.Map<GetListResponse<GetListCourtReservationListItemDto>>(courtReservations);
            return response;
        }
    }
}