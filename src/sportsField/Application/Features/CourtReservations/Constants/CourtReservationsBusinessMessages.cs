namespace Application.Features.CourtReservations.Constants;

public static class CourtReservationsBusinessMessages
{
    public const string SectionName = "CourtReservation";

    public const string CourtReservationNotExists = "CourtReservationNotExists";
    public const string WeekNumberNotMatched = "Bu tarihli randevu kaydedilemez!";
    public const string CannotRented = "Randevu kiralanamaz!";
    public const string ReservationNotCancelled = "Randevu iptal edilemez!";
    public const string WeekNotAvailable = "Hali Haz�rda Bu Hafta Randevu Olu�turulmu�!";
    public const string CourtReservationNotRented = "Reservasyon Kirlanmam��!";
    public const string UserMaxOneOnTheSameDayRenting = "Kullan�c� bir g�nde en fazla bir randevu kiralayabilir.";
    public const string UserMaxFiveReservationPerWeek = "Kullan�c� bir haftada en fazla �� randevu kiralayabilir.";
}