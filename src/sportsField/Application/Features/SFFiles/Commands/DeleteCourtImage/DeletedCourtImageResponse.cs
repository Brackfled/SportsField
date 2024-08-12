using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Commands.DeleteCourtImage;
public class DeletedCourtImageResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
}
