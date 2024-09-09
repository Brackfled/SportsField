using Application.Features.Suspends.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Suspends.Rules;

public class SuspendBusinessRules : BaseBusinessRules
{
    private readonly ISuspendRepository _suspendRepository;
    private readonly ILocalizationService _localizationService;

    public SuspendBusinessRules(ISuspendRepository suspendRepository, ILocalizationService localizationService)
    {
        _suspendRepository = suspendRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SuspendsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SuspendShouldExistWhenSelected(Suspend? suspend)
    {
        if (suspend == null)
            await throwBusinessException(SuspendsBusinessMessages.SuspendNotExists);
    }

    public async Task SuspendIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Suspend? suspend = await _suspendRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SuspendShouldExistWhenSelected(suspend);
    }

    public async Task UserNotBeSuspended(User user)
    {
        ICollection<Suspend> suspends = await _suspendRepository.GetAllAsync(s => s.UserId == user.Id);

        if (suspends.Any(s => s.SuspensionPeriod >= DateTime.UtcNow))
            throw new BusinessException(SuspendsBusinessMessages.UserNotBeSuspended);
    }
}