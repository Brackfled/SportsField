using Application.Features.CourtReservations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Services.OperationClaims;
using Application.Features.OperationClaims.Rules;

namespace Application.Features.CourtReservations.Rules;

public class CourtReservationBusinessRules : BaseBusinessRules
{
    private readonly ICourtReservationRepository _courtReservationRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IOperationClaimService _operationClaimService;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public CourtReservationBusinessRules(ICourtReservationRepository courtReservationRepository, ILocalizationService localizationService, IOperationClaimService operationClaimService, IUserOperationClaimRepository userOperationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _courtReservationRepository = courtReservationRepository;
        _localizationService = localizationService;
        _operationClaimService = operationClaimService;
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CourtReservationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CourtReservationShouldExistWhenSelected(CourtReservation? courtReservation)
    {
        if (courtReservation == null)
            await throwBusinessException(CourtReservationsBusinessMessages.CourtReservationNotExists);
    }

    public async Task CourtReservationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(
            predicate: cr => cr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CourtReservationShouldExistWhenSelected(courtReservation);
    }

    public async Task CourtReservationDateShouldBeMatchedDateNow(DateTime dateTime)
    {
        int requiredWeekNumber = GetDateTimesWeek(dateTime);
        int nowWeekNumber = GetDateTimesWeek(DateTime.UtcNow);

        if (requiredWeekNumber != nowWeekNumber)
            throw new BusinessException(CourtReservationsBusinessMessages.WeekNumberNotMatched);
    }

    private int GetDateTimesWeek(DateTime dateTime)
    {
        DayOfWeek firstDayOfMonth = new DateTime(dateTime.Year, dateTime.Month,1).DayOfWeek;

        int offset = firstDayOfMonth == DayOfWeek.Monday ? 1 : 0;
        int weekNumber = (dateTime.Day + (int)firstDayOfMonth - 1) / 7 + 1;
        return weekNumber;
    }

    public async Task CourtReservationShouldBeActiveAndNotRented(CourtReservation courtReservation)
    {
        if (courtReservation.IsActive == false || courtReservation.UserId != null)
            throw new BusinessException(CourtReservationsBusinessMessages.CannotRented);
    }

    public async Task CourtReservationUserIdAndRequestIdMatched(CourtReservation courtReservation, Guid userId, string operationClaimName)
    {
        OperationClaim? operationClaim = await _operationClaimService.GetAsync(oc => oc.Name == operationClaimName);
        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

        ICollection<OperationClaim>? userOperationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(userId);

        if (courtReservation!.UserId != userId && !userOperationClaims.Contains(operationClaim!))
            throw new BusinessException(CourtReservationsBusinessMessages.ReservationNotCancelled);
    }
}