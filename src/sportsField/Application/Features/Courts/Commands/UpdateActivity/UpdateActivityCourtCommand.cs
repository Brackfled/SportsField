using Amazon.Runtime.Internal;
using Application.Features.Courts.Constants;
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

namespace Application.Features.Courts.Commands.UpdateActivity;
public class UpdateActivityCourtCommand: IRequest<UpdatedActivityCourtResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [CourtsOperationClaims.Admin, CourtsOperationClaims.Update];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourts"];

    public class UpdateActivityCourtCommandHandler: IRequestHandler<UpdateActivityCourtCommand, UpdatedActivityCourtResponse>
    {
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public UpdateActivityCourtCommandHandler(ICourtRepository courtRepository, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedActivityCourtResponse> Handle(UpdateActivityCourtCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtRepository.GetAsync(c => c.Id == request.Id);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, CourtsOperationClaims.Admin);

            court.IsActive = request.IsActive;
            Court updatedCourt = await _courtRepository.UpdateAsync(court);

            UpdatedActivityCourtResponse response = _mapper.Map<UpdatedActivityCourtResponse>(updatedCourt);
            return response;
        }
    }
}
