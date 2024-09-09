using Domain.Enums;

namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserState UserState { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;
    public virtual ICollection<Court>? Courts { get; set; } = default!;
    public virtual ICollection<CourtReservation>? CourtReservations { get; set; } = default!;
    public virtual ICollection<Retention>? Retentions { get; set; } = default!;
    public virtual ICollection<Suspend>? Suspends { get; set; } = default!;
}
