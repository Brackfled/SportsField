using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Commands.UpdateActivity;
public class UpdateActivityCourtReservationCommand: IRequest<UpdatedActivityCourtReservationResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Update];

    public bool BypassCache { get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservation"];

    public class UpdateActivityCourtReservationCommandHandler: IRequestHandler<UpdateActivityCourtReservationCommand, UpdatedActivityCourtReservationResponse>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public UpdateActivityCourtReservationCommandHandler(ICourtReservationRepository courtReservationRepository, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedActivityCourtReservationResponse> Handle(UpdateActivityCourtReservationCommand request, CancellationToken cancellationToken)
        {
            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == request.Id);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(courtReservation!.CourtId, request.UserId, CourtReservationsOperationClaims.Admin);

            courtReservation.IsActive = request.IsActive;
            CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);
            UpdatedActivityCourtReservationResponse response = _mapper.Map<UpdatedActivityCourtReservationResponse>(updatedCourtReservation);
            return response;
        }
    }
}
