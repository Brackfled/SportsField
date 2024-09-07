using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Commands.UpdateMainImage;
public class UpdateMainImageCourtImageResponse
{
    public Guid Id { get; set; }
    public Guid CourtId { get; set; }
    public bool IsMainImage { get; set; }

}
