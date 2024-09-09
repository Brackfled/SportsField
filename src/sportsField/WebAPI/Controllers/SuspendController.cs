using Application.Features.Suspends.Commands.Create;
using Application.Features.Suspends.Commands.Delete;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SuspendController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateSuspend([FromBody] CreateSuspendCommandDto createSuspendCommandDto)
    {
        CreateSuspendCommand command = new() { CreateSuspendCommandDto = createSuspendCommandDto, UserId = getUserIdFromRequest() };
        CreatedSuspendResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSuspend([FromRoute] Guid id)
    {
        DeleteSuspendCommand command = new() { Id = id };
        DeletedSuspendResponse response = await Mediator.Send(command);
        return Ok(response);
    }
}
