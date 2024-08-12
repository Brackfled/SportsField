using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
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

namespace Application.Features.CourtReservations.Commands.RentReservation;
public class RentReservationCommand: IRequest<RentReservationResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid Id {  get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Rent];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservation"];

    public class RentReservationCommandHandler: IRequestHandler<RentReservationCommand, RentReservationResponse>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly IUserService _userService;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly UserBusinessRules _userBusinessRules;
        private IMapper _mapper;

        public RentReservationCommandHandler(ICourtReservationRepository courtReservationRepository, IUserService userService, CourtReservationBusinessRules courtReservationBusinessRules, UserBusinessRules userBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _userService = userService;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _userBusinessRules = userBusinessRules;
            _mapper = mapper;
        }

        public async Task<RentReservationResponse> Handle(RentReservationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == request.Id);
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);
            
            await _courtReservationBusinessRules.CourtReservationShouldBeActiveAndNotRented(courtReservation!);

            courtReservation!.UserId = user!.Id;
            CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);

            RentReservationResponse response = _mapper.Map<RentReservationResponse>(updatedCourtReservation);
            return response;
        }
    }
}
