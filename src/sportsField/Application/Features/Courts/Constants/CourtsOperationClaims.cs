namespace Application.Features.Courts.Constants;

public static class CourtsOperationClaims
{
    private const string _section = "Courts";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string CeoItemsRead = $"CeoItems.Read";
}