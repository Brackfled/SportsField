using FluentValidation;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreateCourtReservationCommandValidator : AbstractValidator<CreateCourtReservationCommand>
{
    public CreateCourtReservationCommandValidator()
    {
    }
}