using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Commands.UpdatePriceRange;
public class UpdatePriceRangeCourtReservationCommand: IRequest<UpdatedPriceRangeCourtReservationResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public IList<UpdatePriceRangeCourtReservationDto> UpdatePriceRangeCourtReservationDtos { get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Create];

    public bool BypassCache { get; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class UpdatePriceRangeCourtReservationCommandHandler: IRequestHandler<UpdatePriceRangeCourtReservationCommand, UpdatedPriceRangeCourtReservationResponse>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private IMapper _mapper;

        public UpdatePriceRangeCourtReservationCommandHandler(ICourtReservationRepository courtReservationRepository, CourtReservationBusinessRules courtReservationBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedPriceRangeCourtReservationResponse> Handle(UpdatePriceRangeCourtReservationCommand request, CancellationToken cancellationToken)
        {
            ICollection<CourtReservation> courtReservations = new List<CourtReservation>();

            foreach (UpdatePriceRangeCourtReservationDto item in request.UpdatePriceRangeCourtReservationDtos)
            {
                CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == item.Id);
                await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

                courtReservation!.Price = item.Price;
                CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);
                courtReservations.Add(updatedCourtReservation);
            }

            UpdatedPriceRangeCourtReservationResponse response = new() { CourtReservations =  courtReservations };
            return response;
        }
    }
}
