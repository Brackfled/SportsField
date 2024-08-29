using Domain.Dtos;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreatedCourtReservationResponse : IResponse
{
    public IList<Guid>? CourtIds { get; set; } = default!;
    public IList<ReservationDetailDto> SavedTimes { get; set; } = default!;
    public IList<ReservationDetailDto> UnsavedTimes { get; set; } = default!;
}