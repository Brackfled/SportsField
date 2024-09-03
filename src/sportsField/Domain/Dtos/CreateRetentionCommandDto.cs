using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class CreateRetentionCommandDto
{
    public string Name { get; set; }
    public CreateCourtReservationCommandDto CreateCourtReservationCommandDto { get; set; }
}
