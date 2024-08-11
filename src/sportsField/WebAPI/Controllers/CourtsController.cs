using Application.Features.Courts.Commands.Create;
using Application.Features.Courts.Commands.Delete;
using Application.Features.Courts.Commands.Update;
using Application.Features.Courts.Queries.GetById;
using Application.Features.Courts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourtsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCourtResponse>> Add([FromBody] CreateCourtCommand command)
    {
        CreatedCourtResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedCourtResponse>> Update([FromBody] UpdateCourtCommand command)
    {
        UpdatedCourtResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCourtResponse>> Delete([FromRoute] Guid id)
    {
        DeleteCourtCommand command = new() { Id = id };

        DeletedCourtResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCourtResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdCourtQuery query = new() { Id = id };

        GetByIdCourtResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListCourtQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCourtQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListCourtListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}