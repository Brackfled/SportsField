using NArchitecture.Core.Application.Dtos;

namespace Application.Features.CourtReservations.Queries.GetList;

public class GetListCourtReservationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid CourtId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public long CreatedTime { get; set; }
    public bool IsActive { get; set; }
    public int Price { get; set; }
}