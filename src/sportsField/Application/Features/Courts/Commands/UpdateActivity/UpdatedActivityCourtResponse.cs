using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Commands.UpdateActivity;
public class UpdatedActivityCourtResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}
