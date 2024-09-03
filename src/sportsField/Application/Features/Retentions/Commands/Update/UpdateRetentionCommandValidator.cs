using FluentValidation;

namespace Application.Features.Retentions.Commands.Update;

public class UpdateRetentionCommandValidator : AbstractValidator<UpdateRetentionCommand>
{
    public UpdateRetentionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Command).NotEmpty();
    }
}