using Application.Features.Suspends.Constants;
using Application.Features.Suspends.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;

namespace Application.Features.Suspends.Commands.Delete;

public class DeleteSuspendCommand : IRequest<DeletedSuspendResponse>, ITransactionalRequest, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [SuspendsOperationClaims.Admin];

    public class DeleteSuspendCommandHandler : IRequestHandler<DeleteSuspendCommand, DeletedSuspendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISuspendRepository _suspendRepository;
        private readonly SuspendBusinessRules _suspendBusinessRules;

        public DeleteSuspendCommandHandler(IMapper mapper, ISuspendRepository suspendRepository,
                                         SuspendBusinessRules suspendBusinessRules)
        {
            _mapper = mapper;
            _suspendRepository = suspendRepository;
            _suspendBusinessRules = suspendBusinessRules;
        }

        public async Task<DeletedSuspendResponse> Handle(DeleteSuspendCommand request, CancellationToken cancellationToken)
        {
            Suspend? suspend = await _suspendRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _suspendBusinessRules.SuspendShouldExistWhenSelected(suspend);

            await _suspendRepository.DeleteAsync(suspend!, true);

            DeletedSuspendResponse response = _mapper.Map<DeletedSuspendResponse>(suspend);
            return response;
        }
    }
}