using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Queries.GetListByDynamic;
public class GetListByDynamicCourtReservationListItemDto
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
