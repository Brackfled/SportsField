using Application.Features.Courts.Constants;
using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Courts.Constants.CourtsOperationClaims;
using Domain.Dtos;

namespace Application.Features.Courts.Commands.Update;

public class UpdateCourtCommand : IRequest<UpdatedCourtResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public UpdateCourtCommandDto UpdateCourtCommandDto { get; set; }

    public string[] Roles => [Admin, CourtsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourts"];

    public class UpdateCourtCommandHandler : IRequestHandler<UpdateCourtCommand, UpdatedCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;

        public UpdateCourtCommandHandler(IMapper mapper, ICourtRepository courtRepository,
                                         CourtBusinessRules courtBusinessRules)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<UpdatedCourtResponse> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtRepository.GetAsync(predicate: c => c.Id == request.UpdateCourtCommandDto.Id, cancellationToken: cancellationToken);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, Admin);
            court = _mapper.Map(request.UpdateCourtCommandDto, court);

            await _courtRepository.UpdateAsync(court!);

            UpdatedCourtResponse response = _mapper.Map<UpdatedCourtResponse>(court);
            return response;
        }
    }
}