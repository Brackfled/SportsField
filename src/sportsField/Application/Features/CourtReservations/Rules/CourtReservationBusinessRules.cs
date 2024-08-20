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
        // T�rkiye'de haftan�n Pazartesi g�n�nden ba�lamas� ve haftalar�n y�l baz�nda hesaplanmas� i�in ISO 8601 standard�na uygun �ekilde:
        var culture = new System.Globalization.CultureInfo("tr-TR");
        var calendar = culture.Calendar;

        // Haftan�n Pazartesi g�n� ba�lad��� bir kuralla hesaplama yapmak i�in:
        var firstDayOfWeek = DayOfWeek.Monday;
        var weekRule = System.Globalization.CalendarWeekRule.FirstFourDayWeek; // Haftan�n en az 4 g�n�n� i�eren ilk hafta

        // Haftan�n y�l i�erisindeki numaras�n� elde etmek i�in:
        int weekNumber = calendar.GetWeekOfYear(dateTime, weekRule, firstDayOfWeek);

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

    public async Task<(IList<string> saveTimes, IList<string> unsaveTimes)> ReservationsTimeControl(IList<string> reservationTimes)
    {
        IList<string> saveTimes = new List<string>();
        IList<string> unsaveTimes = new List<string>();
        IList<(TimeSpan startTime, TimeSpan endTime)> reservationTimesTuple = new List<(TimeSpan startTime, TimeSpan endTime)>();

        foreach (string item in reservationTimes)
        {
            string[] times = item.Split("-");
            TimeSpan startTime = TimeSpan.Parse(times[0]);
            TimeSpan endTime = TimeSpan.Parse(times[1]);

            if (reservationTimesTuple.Count == 0)
            {
                saveTimes.Add(item);
                continue;
            }

            bool result = reservationTimesTuple.Any(t =>
                             (startTime >= t.startTime && startTime < t.endTime) ||
                             (endTime > t.startTime && endTime <= t.endTime) ||
                             (startTime <= t.startTime && endTime >= t.endTime)
                           );

            if (!result)
            {
                saveTimes.Add(item);
                reservationTimesTuple.Add((startTime, endTime));
            }

            if(result)
                unsaveTimes.Add(item);
        }

        return (saveTimes, unsaveTimes);

    }
}