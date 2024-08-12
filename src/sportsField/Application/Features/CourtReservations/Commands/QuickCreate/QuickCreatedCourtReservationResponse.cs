using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Commands.QuickCreate;
public class QuickCreatedCourtReservationResponse
{
    public Guid CourtId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public long CreatedTime { get; set; }
    public bool IsActive { get; set; }
}
