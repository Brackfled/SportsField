using FluentValidation;

namespace Application.Features.Courts.Commands.Delete;

public class DeleteCourtCommandValidator : AbstractValidator<DeleteCourtCommand>
{
    public DeleteCourtCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}