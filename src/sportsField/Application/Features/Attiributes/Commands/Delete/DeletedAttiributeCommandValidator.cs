using FluentValidation;

namespace Application.Features.Attiributes.Commands.Delete;

public class DeleteAttiributeCommandValidator : AbstractValidator<DeleteAttiributeCommand>
{
    public DeleteAttiributeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}