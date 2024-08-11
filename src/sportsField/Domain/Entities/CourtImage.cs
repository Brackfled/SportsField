using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CourtImage: SFFile
{
    public Guid CourtId { get; set; }

    public virtual Court? Court { get; set; }
}
