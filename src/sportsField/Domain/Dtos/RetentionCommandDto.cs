using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class RetentionCommandDto
{
    public IList<Guid> CourtIds { get; set; }
    public IList<string> ReservationDays { get; set; }
    public IList<ReservationDetailDto> ReservationDetailDtos { get; set; }
}
