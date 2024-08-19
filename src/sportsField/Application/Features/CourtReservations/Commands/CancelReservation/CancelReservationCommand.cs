using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
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

namespace Application.Features.CourtReservations.Commands.CancelReservation;
public class CancelReservationCommand: IRequest<CancelledReservationResponse>, ITransactionalRequest, ICacheRemoverRequest, ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Cancel];

    public class CancelReservationCommandHandler: IRequestHandler<CancelReservationCommand, CancelledReservationResponse>
    {
        private readonly IUserService _userService;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public CancelReservationCommandHandler(IUserService userService, ICourtReservationRepository courtReservationRepository, UserBusinessRules userBusinessRules, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _userService = userService;
            _courtReservationRepository = courtReservationRepository;
            _userBusinessRules = userBusinessRules;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<CancelledReservationResponse> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == request.Id);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

            await _courtReservationBusinessRules.CourtReservationUserIdAndRequestIdMatched(courtReservation!, request.UserId, CourtReservationsOperationClaims.Cancel);

            courtReservation!.UserId = null;
            CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);

            CancelledReservationResponse response = _mapper.Map<CancelledReservationResponse>(updatedCourtReservation);
            return response;
        }
    }
}
