using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos;
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

namespace Application.Features.CourtReservations.Commands.QuickCreate;
public class QuickCreateCourtReservationCommand: IRequest<QuickCreatedCourtReservationResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public QuickCreateCourtReservationCommandDto QuickCreateCourtReservationCommandDto { get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Create];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class QuickCreateCourtReservationCommandHandler: IRequestHandler<QuickCreateCourtReservationCommand, QuickCreatedCourtReservationResponse>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly ICourtService _courtService;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private IMapper _mapper;

        public QuickCreateCourtReservationCommandHandler(ICourtReservationRepository courtReservationRepository, ICourtService courtService, CourtBusinessRules courtBusinessRules, CourtReservationBusinessRules courtReservationBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _courtService = courtService;
            _courtBusinessRules = courtBusinessRules;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _mapper = mapper;
        }

        public async Task<QuickCreatedCourtReservationResponse> Handle(QuickCreateCourtReservationCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtService.GetAsync(c => c.Id == request.QuickCreateCourtReservationCommandDto.CourtId);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, CourtReservationsOperationClaims.Admin);

            string[] times = request.QuickCreateCourtReservationCommandDto.Times.Split("-");

            await _courtReservationBusinessRules.CourtReservationDateShouldBeMatchedDateNow(request.QuickCreateCourtReservationCommandDto.AvailableDate);

            CourtReservation courtReservation = new()
            {
                Id = Guid.NewGuid(),
                UserId = null,
                AvailableDate = request.QuickCreateCourtReservationCommandDto.AvailableDate.Date,
                StartTime = TimeSpan.Parse(times[0]),
                EndTime = TimeSpan.Parse(times[1]),
                CreatedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                CourtId = court!.Id,
                IsActive = request.QuickCreateCourtReservationCommandDto.IsActive,
            };

            CourtReservation addedCourtReservation = await _courtReservationRepository.AddAsync(courtReservation);
            QuickCreatedCourtReservationResponse response = _mapper.Map<QuickCreatedCourtReservationResponse>(addedCourtReservation);
            return response;
        }
    }
}
