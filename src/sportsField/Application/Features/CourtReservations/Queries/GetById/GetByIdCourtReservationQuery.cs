using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CourtReservations.Constants.CourtReservationsOperationClaims;

namespace Application.Features.CourtReservations.Queries.GetById;

public class GetByIdCourtReservationQuery : IRequest<GetByIdCourtReservationResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCourtReservationQueryHandler : IRequestHandler<GetByIdCourtReservationQuery, GetByIdCourtReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

        public GetByIdCourtReservationQueryHandler(IMapper mapper, ICourtReservationRepository courtReservationRepository, CourtReservationBusinessRules courtReservationBusinessRules)
        {
            _mapper = mapper;
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
        }

        public async Task<GetByIdCourtReservationResponse> Handle(GetByIdCourtReservationQuery request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(predicate: cr => cr.Id == request.Id, cancellationToken: cancellationToken);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

            GetByIdCourtReservationResponse response = _mapper.Map<GetByIdCourtReservationResponse>(courtReservation);
            return response;
        }
    }
}