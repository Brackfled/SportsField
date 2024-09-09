using FluentValidation;

namespace Application.Features.Suspends.Commands.Create;

public class CreateSuspendCommandValidator : AbstractValidator<CreateSuspendCommand>
{
    public CreateSuspendCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}