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

namespace Application.Features.Courts.Commands.Update;

public class UpdateCourtCommand : IRequest<UpdatedCourtResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required CourtType CourtType { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }
    public required string Lat { get; set; }
    public required string Lng { get; set; }
    public required string FormattedAddress { get; set; }

    public string[] Roles => [Admin, Write, CourtsOperationClaims.Update];

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
            Court? court = await _courtRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            court = _mapper.Map(request, court);

            await _courtRepository.UpdateAsync(court!);

            UpdatedCourtResponse response = _mapper.Map<UpdatedCourtResponse>(court);
            return response;
        }
    }
}