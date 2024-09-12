namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string SectionName = "Auth";

    public const string EmailAuthenticatorDontExists = "EmailAuthenticatorDontExists";
    public const string OtpAuthenticatorDontExists = "OtpAuthenticatorDontExists";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "AlreadyVerifiedOtpAuthenticatorIsExists";
    public const string EmailActivationKeyDontExists = "EmailActivationKeyDontExists";
    public const string UserDontExists = "Kullanýcu Bulunamadý";
    public const string UserHaveAlreadyAAuthenticator = "UserHaveAlreadyAAuthenticator";
    public const string RefreshDontExists = "RefreshDontExists";
    public const string InvalidRefreshToken = "InvalidRefreshToken";
    public const string UserMailAlreadyExists = "E-Posta Mevcut";
    public const string PasswordDontMatch = "Kullanýcý adý veya þifre yanlýþ!";
}
