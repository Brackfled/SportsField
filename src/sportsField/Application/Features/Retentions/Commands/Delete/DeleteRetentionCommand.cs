using Application.Features.Retentions.Constants;
using Application.Features.Retentions.Constants;
using Application.Features.Retentions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Retentions.Constants.RetentionsOperationClaims;

namespace Application.Features.Retentions.Commands.Delete;

public class DeleteRetentionCommand : IRequest<DeletedRetentionResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, RetentionsOperationClaims.Delete];

    public class DeleteRetentionCommandHandler : IRequestHandler<DeleteRetentionCommand, DeletedRetentionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRetentionRepository _retentionRepository;
        private readonly RetentionBusinessRules _retentionBusinessRules;

        public DeleteRetentionCommandHandler(IMapper mapper, IRetentionRepository retentionRepository,
                                         RetentionBusinessRules retentionBusinessRules)
        {
            _mapper = mapper;
            _retentionRepository = retentionRepository;
            _retentionBusinessRules = retentionBusinessRules;
        }

        public async Task<DeletedRetentionResponse> Handle(DeleteRetentionCommand request, CancellationToken cancellationToken)
        {
            Retention? retention = await _retentionRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _retentionBusinessRules.RetentionShouldExistWhenSelected(retention);

            await _retentionRepository.DeleteAsync(retention!, true);

            DeletedRetentionResponse response = _mapper.Map<DeletedRetentionResponse>(retention);
            return response;
        }
    }
}