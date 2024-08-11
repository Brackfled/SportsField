using FluentValidation;

namespace Application.Features.Attiributes.Commands.Create;

public class CreateAttiributeCommandValidator : AbstractValidator<CreateAttiributeCommand>
{
    public CreateAttiributeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}