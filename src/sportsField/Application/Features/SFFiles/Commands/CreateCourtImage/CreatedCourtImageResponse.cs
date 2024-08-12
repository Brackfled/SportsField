using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Commands.CreateCourtImage;
public class CreatedCourtImageResponse
{
    public ICollection<CourtImage>? CourtImages { get; set; } = default!;
}
