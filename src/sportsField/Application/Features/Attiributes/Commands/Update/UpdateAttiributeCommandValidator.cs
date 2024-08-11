using FluentValidation;

namespace Application.Features.Attiributes.Commands.Update;

public class UpdateAttiributeCommandValidator : AbstractValidator<UpdateAttiributeCommand>
{
    public UpdateAttiributeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}