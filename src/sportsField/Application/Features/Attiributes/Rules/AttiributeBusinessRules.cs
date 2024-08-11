using Application.Features.Attiributes.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Attiributes.Rules;

public class AttiributeBusinessRules : BaseBusinessRules
{
    private readonly IAttiributeRepository _attiributeRepository;
    private readonly ILocalizationService _localizationService;

    public AttiributeBusinessRules(IAttiributeRepository attiributeRepository, ILocalizationService localizationService)
    {
        _attiributeRepository = attiributeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AttiributesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AttiributeShouldExistWhenSelected(Attiribute? attiribute)
    {
        if (attiribute == null)
            await throwBusinessException(AttiributesBusinessMessages.AttiributeNotExists);
    }

    public async Task AttiributeIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Attiribute? attiribute = await _attiributeRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AttiributeShouldExistWhenSelected(attiribute);
    }
}