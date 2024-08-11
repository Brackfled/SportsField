using FluentValidation;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreateCourtReservationCommandValidator : AbstractValidator<CreateCourtReservationCommand>
{
    public CreateCourtReservationCommandValidator()
    {
        RuleFor(c => c.CourtId).NotEmpty();
        RuleFor(c => c.AvailableDate).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
        RuleFor(c => c.CreatedTime).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}