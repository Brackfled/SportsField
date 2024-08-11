using Application.Features.Courts.Constants;
using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Courts.Constants.CourtsOperationClaims;

namespace Application.Features.Courts.Queries.GetById;

public class GetByIdCourtQuery : IRequest<GetByIdCourtResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCourtQueryHandler : IRequestHandler<GetByIdCourtQuery, GetByIdCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly CourtBusinessRules _courtBusinessRules;

        public GetByIdCourtQueryHandler(IMapper mapper, ICourtRepository courtRepository, CourtBusinessRules courtBusinessRules)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<GetByIdCourtResponse> Handle(GetByIdCourtQuery request, CancellationToken cancellationToken)
        {
            Court? court = await _courtRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);

            GetByIdCourtResponse response = _mapper.Map<GetByIdCourtResponse>(court);
            return response;
        }
    }
}