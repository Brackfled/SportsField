using Application.Features.Suspends.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Suspends.Queries.GetById;

public class GetByIdSuspendQuery : IRequest<GetByIdSuspendResponse>
{
    public Guid Id { get; set; }

    public class GetByIdSuspendQueryHandler : IRequestHandler<GetByIdSuspendQuery, GetByIdSuspendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISuspendRepository _suspendRepository;
        private readonly SuspendBusinessRules _suspendBusinessRules;

        public GetByIdSuspendQueryHandler(IMapper mapper, ISuspendRepository suspendRepository, SuspendBusinessRules suspendBusinessRules)
        {
            _mapper = mapper;
            _suspendRepository = suspendRepository;
            _suspendBusinessRules = suspendBusinessRules;
        }

        public async Task<GetByIdSuspendResponse> Handle(GetByIdSuspendQuery request, CancellationToken cancellationToken)
        {
            Suspend? suspend = await _suspendRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _suspendBusinessRules.SuspendShouldExistWhenSelected(suspend);

            GetByIdSuspendResponse response = _mapper.Map<GetByIdSuspendResponse>(suspend);
            return response;
        }
    }
}