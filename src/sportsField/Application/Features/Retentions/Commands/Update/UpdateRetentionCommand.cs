using Application.Features.Retentions.Constants;
using Application.Features.Retentions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Retentions.Constants.RetentionsOperationClaims;

namespace Application.Features.Retentions.Commands.Update;

public class UpdateRetentionCommand : IRequest<UpdatedRetentionResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Command { get; set; }

    public string[] Roles => [Admin, Write, RetentionsOperationClaims.Update];

    public class UpdateRetentionCommandHandler : IRequestHandler<UpdateRetentionCommand, UpdatedRetentionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRetentionRepository _retentionRepository;
        private readonly RetentionBusinessRules _retentionBusinessRules;

        public UpdateRetentionCommandHandler(IMapper mapper, IRetentionRepository retentionRepository,
                                         RetentionBusinessRules retentionBusinessRules)
        {
            _mapper = mapper;
            _retentionRepository = retentionRepository;
            _retentionBusinessRules = retentionBusinessRules;
        }

        public async Task<UpdatedRetentionResponse> Handle(UpdateRetentionCommand request, CancellationToken cancellationToken)
        {
            Retention? retention = await _retentionRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _retentionBusinessRules.RetentionShouldExistWhenSelected(retention);
            retention = _mapper.Map(request, retention);

            await _retentionRepository.UpdateAsync(retention!);

            UpdatedRetentionResponse response = _mapper.Map<UpdatedRetentionResponse>(retention);
            return response;
        }
    }
}