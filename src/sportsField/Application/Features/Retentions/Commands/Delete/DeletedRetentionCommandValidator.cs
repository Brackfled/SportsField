using FluentValidation;

namespace Application.Features.Retentions.Commands.Delete;

public class DeleteRetentionCommandValidator : AbstractValidator<DeleteRetentionCommand>
{
    public DeleteRetentionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}