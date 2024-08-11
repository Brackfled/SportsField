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

namespace Application.Features.Courts.Commands.Create;

public class CreateCourtCommand : IRequest<CreatedCourtResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required CourtType CourtType { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }
    public required string Lat { get; set; }
    public required string Lng { get; set; }
    public required string FormattedAddress { get; set; }

    public string[] Roles => [Admin, Write, CourtsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourts"];

    public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, CreatedCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;

        public CreateCourtCommandHandler(IMapper mapper, ICourtRepository courtRepository,
                                         CourtBusinessRules courtBusinessRules)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<CreatedCourtResponse> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            Court court = _mapper.Map<Court>(request);

            await _courtRepository.AddAsync(court);

            CreatedCourtResponse response = _mapper.Map<CreatedCourtResponse>(court);
            return response;
        }
    }
}