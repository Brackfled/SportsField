using Application.Features.Courts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Services.OperationClaims;
using Application.Features.OperationClaims.Rules;
using Application.Services.UserOperationClaims;

namespace Application.Features.Courts.Rules;

public class CourtBusinessRules : BaseBusinessRules
{
    private readonly ICourtRepository _courtRepository;
    private readonly IOperationClaimService _operationClaimService;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
    private readonly ILocalizationService _localizationService;

    public CourtBusinessRules(ICourtRepository courtRepository, IOperationClaimService operationClaimService, IUserOperationClaimRepository userOperationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules, ILocalizationService localizationService)
    {
        _courtRepository = courtRepository;
        _operationClaimService = operationClaimService;
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
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

    public async Task UserIdNotMatchedCourtUserId(Guid courtId, Guid userId, string operationClaimName)
    {
        Court? court = await _courtRepository.GetAsync(c => c.Id == courtId);
        await CourtShouldExistWhenSelected(court);

        OperationClaim? operationClaim = await _operationClaimService.GetAsync(oc => oc.Name == operationClaimName);
        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

        ICollection<OperationClaim>? operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(userId);

        if (court!.UserId != userId && !operationClaims.Contains(operationClaim!))
            throw new BusinessException(CourtsBusinessMessages.UserIdNotMatchedCourtUserId);

    }
}