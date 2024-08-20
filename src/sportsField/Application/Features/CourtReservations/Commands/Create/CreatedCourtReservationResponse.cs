using NArchitecture.Core.Application.Responses;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreatedCourtReservationResponse : IResponse
{
    public IList<Guid>? CourtIds { get; set; } = default!;
    public IList<string> SavedTimes { get; set; } = default!;
    public IList<string> UnsavedTimes { get; set; } = default!;
}