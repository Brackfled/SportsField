using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListCeoItems;
public class GetListCeoItemsDto
{
    public int TotalCourtsCount { get; set; }
    public int ActiveCourtsCount { get; set; }
    public int TotalUserCount { get; set; }
    public int TotalCourtOwnerCount { get; set; }
    public int TotalReservationCount { get; set; }
    public int ReservedReservationCount { get; set; }
}
