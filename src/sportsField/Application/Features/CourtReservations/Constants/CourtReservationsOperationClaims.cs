namespace Application.Features.CourtReservations.Constants;

public static class CourtReservationsOperationClaims
{
    private const string _section = "CourtReservations";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string Rent = $"{_section}.Rent";
    public const string Cancel = $"{_section}.Cancel";
}