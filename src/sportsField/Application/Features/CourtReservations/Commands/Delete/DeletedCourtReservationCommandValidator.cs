using FluentValidation;

namespace Application.Features.CourtReservations.Commands.Delete;

public class DeleteCourtReservationCommandValidator : AbstractValidator<DeleteCourtReservationCommand>
{
    public DeleteCourtReservationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}