using NArchitecture.Core.Application.Responses;

namespace Application.Features.CourtReservations.Commands.Delete;

public class DeletedCourtReservationResponse : IResponse
{
    public Guid Id { get; set; }
}