using NArchitecture.Core.Application.Responses;

namespace Application.Features.CourtReservations.Commands.Create;

public class CreatedCourtReservationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CourtId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public long CreatedTime { get; set; }
    public bool IsActive { get; set; }
}