using Application.Features.Attiributes.Constants;
using Application.Features.Attiributes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Attiributes.Constants.AttiributesOperationClaims;

namespace Application.Features.Attiributes.Queries.GetById;

public class GetByIdAttiributeQuery : IRequest<GetByIdAttiributeResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAttiributeQueryHandler : IRequestHandler<GetByIdAttiributeQuery, GetByIdAttiributeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttiributeRepository _attiributeRepository;
        private readonly AttiributeBusinessRules _attiributeBusinessRules;

        public GetByIdAttiributeQueryHandler(IMapper mapper, IAttiributeRepository attiributeRepository, AttiributeBusinessRules attiributeBusinessRules)
        {
            _mapper = mapper;
            _attiributeRepository = attiributeRepository;
            _attiributeBusinessRules = attiributeBusinessRules;
        }

        public async Task<GetByIdAttiributeResponse> Handle(GetByIdAttiributeQuery request, CancellationToken cancellationToken)
        {
            Attiribute? attiribute = await _attiributeRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _attiributeBusinessRules.AttiributeShouldExistWhenSelected(attiribute);

            GetByIdAttiributeResponse response = _mapper.Map<GetByIdAttiributeResponse>(attiribute);
            return response;
        }
    }
}