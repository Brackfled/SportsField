using Amazon.Runtime.Internal;
using Application.Features.Courts.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Queries.GetListById;
public class GetListByIdCourtReservationQuery: IRequest<ICollection<CourtReservation>>, ICachableRequest
{
    public Guid CourtId { get; set; }

    public bool BypassCache {  get; set; }

    public string CacheKey => $"GetListById/{CourtId}";

    public string? CacheGroupKey => "GetCourtReservations";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListByIdCourtReservationQueryHandler: IRequestHandler<GetListByIdCourtReservationQuery, ICollection<CourtReservation>>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly ICourtService _courtService;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public GetListByIdCourtReservationQueryHandler(ICourtReservationRepository courtReservationRepository, ICourtService courtService, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _courtService = courtService;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<CourtReservation>> Handle(GetListByIdCourtReservationQuery request, CancellationToken cancellationToken)
        {
            Court? court = await _courtService.GetAsync(c => c.Id == request.CourtId);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);

            ICollection<CourtReservation> courtReservations = await _courtReservationRepository.GetAllAsync(cr => cr.CourtId == court!.Id);

            return courtReservations;
        }
    }
}
