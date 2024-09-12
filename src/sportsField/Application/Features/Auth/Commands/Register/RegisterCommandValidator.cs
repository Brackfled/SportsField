using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.RegisterCommandDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.RegisterCommandDto.FirstName).NotEmpty();
        RuleFor(c => c.RegisterCommandDto.LastName).NotEmpty();
        RuleFor(c => c.RegisterCommandDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Şifre, en az birer adet büyük harf, küçük harf, rakam ve özel karakter içermelidir."
            );
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
