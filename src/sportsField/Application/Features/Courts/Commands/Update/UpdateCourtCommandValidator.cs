using FluentValidation;

namespace Application.Features.Courts.Commands.Update;

public class UpdateCourtCommandValidator : AbstractValidator<UpdateCourtCommand>
{
    public UpdateCourtCommandValidator()
    {
        RuleFor(c => c.UpdateCourtCommandDto.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.Name).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.CourtType).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.Description).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.IsActive).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.Lat).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.Lng).NotEmpty();
        RuleFor(c => c.UpdateCourtCommandDto.FormattedAddress).NotEmpty();
    }
}