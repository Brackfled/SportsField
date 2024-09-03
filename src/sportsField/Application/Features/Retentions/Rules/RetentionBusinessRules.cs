using Application.Features.Retentions.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Retentions.Rules;

public class RetentionBusinessRules : BaseBusinessRules
{
    private readonly IRetentionRepository _retentionRepository;
    private readonly ILocalizationService _localizationService;

    public RetentionBusinessRules(IRetentionRepository retentionRepository, ILocalizationService localizationService)
    {
        _retentionRepository = retentionRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, RetentionsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task RetentionShouldExistWhenSelected(Retention? retention)
    {
        if (retention == null)
            await throwBusinessException(RetentionsBusinessMessages.RetentionNotExists);
    }

    public async Task RetentionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Retention? retention = await _retentionRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await RetentionShouldExistWhenSelected(retention);
    }
}