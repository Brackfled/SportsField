using Application.Features.CourtReservations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Services.OperationClaims;
using Application.Features.OperationClaims.Rules;
using Domain.Dtos;

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
        // Türkiye'de haftanýn Pazartesi gününden baþlamasý ve haftalarýn yýl bazýnda hesaplanmasý için ISO 8601 standardýna uygun þekilde:
        var culture = new System.Globalization.CultureInfo("tr-TR");
        var calendar = culture.Calendar;

        // Haftanýn Pazartesi günü baþladýðý bir kuralla hesaplama yapmak için:
        var firstDayOfWeek = DayOfWeek.Monday;
        var weekRule = System.Globalization.CalendarWeekRule.FirstFourDayWeek; // Haftanýn en az 4 gününü içeren ilk hafta

        // Haftanýn yýl içerisindeki numarasýný elde etmek için:
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

    public async Task CourtShouldBeAvailableWeek(Court court)
    {
        ICollection<CourtReservation>? courtReservations = await _courtReservationRepository.GetAllAsync(cr => cr.CourtId == court.Id);
        int nowDate = GetDateTimesWeek(DateTime.UtcNow);

        foreach (CourtReservation courtReservation in courtReservations)
        {
            int crDate = GetDateTimesWeek(courtReservation.AvailableDate);

            if (crDate == nowDate)
                throw new BusinessException(CourtReservationsBusinessMessages.WeekNotAvailable);
        }
    }

    public async Task<(IList<ReservationDetailDto> saveTimes, IList<ReservationDetailDto> unsaveTimes)> ReservationsTimeControl(IList<ReservationDetailDto> reservationTimes)
    {
        IList<ReservationDetailDto> saveTimes = new List<ReservationDetailDto>();
        IList<ReservationDetailDto> unsaveTimes = new List<ReservationDetailDto>();
        IList<(TimeSpan startTime, TimeSpan endTime)> reservationTimesTuple = new List<(TimeSpan startTime, TimeSpan endTime)>();

        foreach (ReservationDetailDto item in reservationTimes)
        {
            string[] times = item.Times.Split("-");
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

    public async Task CourtReservationShouldBeRented(CourtReservation courtReservation)
    {
        if (courtReservation.UserId == null)
            throw new BusinessException(CourtReservationsBusinessMessages.CourtReservationNotRented);
    }
}