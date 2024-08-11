using FluentValidation;

namespace Application.Features.Courts.Commands.Create;

public class CreateCourtCommandValidator : AbstractValidator<CreateCourtCommand>
{
    public CreateCourtCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.CourtType).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.Lat).NotEmpty();
        RuleFor(c => c.Lng).NotEmpty();
        RuleFor(c => c.FormattedAddress).NotEmpty();
    }
}