using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class CreateCourtCommandDto
{
    public  string Name { get; set; }
    public  CourtType CourtType { get; set; }
    public  string Description { get; set; }
    public  string Lat { get; set; }
    public  string Lng { get; set; }
    public  string FormattedAddress { get; set; }
    public IList<string> AttiributeNames { get; set; }
}
