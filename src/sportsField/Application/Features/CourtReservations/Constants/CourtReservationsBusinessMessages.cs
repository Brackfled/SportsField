namespace Application.Features.CourtReservations.Constants;

public static class CourtReservationsBusinessMessages
{
    public const string SectionName = "CourtReservation";

    public const string CourtReservationNotExists = "CourtReservationNotExists";
    public const string WeekNumberNotMatched = "Bu tarihli randevu kaydedilemez!";
    public const string CannotRented = "Randevu kiralanamaz!";
    public const string ReservationNotCancelled = "Randevu iptal edilemez!";
    public const string WeekNotAvailable = "Hali Hazýrda Bu Hafta Randevu Oluþturulmuþ!";
    public const string CourtReservationNotRented = "Reservasyon Kirlanmamýþ!";
}