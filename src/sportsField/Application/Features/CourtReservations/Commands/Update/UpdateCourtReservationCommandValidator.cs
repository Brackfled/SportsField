using FluentValidation;

namespace Application.Features.CourtReservations.Commands.Update;

public class UpdateCourtReservationCommandValidator : AbstractValidator<UpdateCourtReservationCommand>
{
    public UpdateCourtReservationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CourtId).NotEmpty();
        RuleFor(c => c.AvailableDate).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
        RuleFor(c => c.CreatedTime).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}