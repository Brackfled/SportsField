using Application.Features.SFFiles.Commands.CreateCourtImage;
using Application.Features.SFFiles.Commands.DeleteCourtImage;
using Application.Features.SFFiles.Commands.UpdateMainImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SFFilesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateCourtImage([FromForm] IList<IFormFile> formFiles, Guid courtId)
    {
        CreateCourtImageCommand command = new() { UserId = getUserIdFromRequest(), FormFiles = formFiles , CourtId  = courtId};
        CreatedCourtImageResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourtImage([FromRoute] Guid id) 
    {
        DeleteCourtImageCommand command = new() { Id = id , UserId = getUserIdFromRequest()};
        DeletedCourtImageResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("UpdateCourtImageMainImage")]
    public async Task<IActionResult> UpdateMainImageCourtImage([FromRoute] Guid courtId, [FromBody]Guid id)
    {
        UpdateMainImageCourtImageCommand command = new() { UserId = getUserIdFromRequest(), CourtId = courtId, Id = id };
        UpdateMainImageCourtImageResponse response = await Mediator.Send(command);
        return Ok(response);
    }
}
