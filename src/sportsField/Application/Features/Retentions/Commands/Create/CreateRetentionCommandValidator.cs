using FluentValidation;

namespace Application.Features.Retentions.Commands.Create;

public class CreateRetentionCommandValidator : AbstractValidator<CreateRetentionCommand>
{
    public CreateRetentionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.CreateRetentionCommandDto.Name).NotEmpty();
    }
}