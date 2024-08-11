using Application.Features.Courts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Courts.Rules;

public class CourtBusinessRules : BaseBusinessRules
{
    private readonly ICourtRepository _courtRepository;
    private readonly ILocalizationService _localizationService;

    public CourtBusinessRules(ICourtRepository courtRepository, ILocalizationService localizationService)
    {
        _courtRepository = courtRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CourtsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CourtShouldExistWhenSelected(Court? court)
    {
        if (court == null)
            await throwBusinessException(CourtsBusinessMessages.CourtNotExists);
    }

    public async Task CourtIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Court? court = await _courtRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CourtShouldExistWhenSelected(court);
    }
}