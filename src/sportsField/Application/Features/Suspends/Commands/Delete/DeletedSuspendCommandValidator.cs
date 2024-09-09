using FluentValidation;

namespace Application.Features.Suspends.Commands.Delete;

public class DeleteSuspendCommandValidator : AbstractValidator<DeleteSuspendCommand>
{
    public DeleteSuspendCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}