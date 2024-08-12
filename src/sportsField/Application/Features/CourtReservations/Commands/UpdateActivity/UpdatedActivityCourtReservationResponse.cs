using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Commands.UpdateActivity;
public class UpdatedActivityCourtReservationResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsActivity { get; set; }
}
