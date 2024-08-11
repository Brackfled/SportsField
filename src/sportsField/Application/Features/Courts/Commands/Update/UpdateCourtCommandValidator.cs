using FluentValidation;

namespace Application.Features.Courts.Commands.Update;

public class UpdateCourtCommandValidator : AbstractValidator<UpdateCourtCommand>
{
    public UpdateCourtCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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