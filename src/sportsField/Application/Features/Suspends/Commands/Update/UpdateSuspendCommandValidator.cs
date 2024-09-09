using FluentValidation;

namespace Application.Features.Suspends.Commands.Update;

public class UpdateSuspendCommandValidator : AbstractValidator<UpdateSuspendCommand>
{
    public UpdateSuspendCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.SuspensionPeriod).NotEmpty();
        RuleFor(c => c.Reason).NotEmpty();
    }
}