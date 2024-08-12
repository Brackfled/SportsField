using FluentValidation;

namespace Application.Features.Courts.Commands.Create;

public class CreateCourtCommandValidator : AbstractValidator<CreateCourtCommand>
{
    public CreateCourtCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.CreateCourtCommandDto.Name).NotEmpty();
        RuleFor(c => c.CreateCourtCommandDto.CourtType);
        RuleFor(c => c.CreateCourtCommandDto.Description).NotEmpty();
        RuleFor(c => c.CreateCourtCommandDto.Lat).NotEmpty();
        RuleFor(c => c.CreateCourtCommandDto.Lng).NotEmpty();
        RuleFor(c => c.CreateCourtCommandDto.FormattedAddress).NotEmpty();
    }
}