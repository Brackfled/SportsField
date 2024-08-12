using Application.Features.Courts.Commands.Create;
using Application.Features.Courts.Commands.Delete;
using Application.Features.Courts.Commands.Update;
using Application.Features.Courts.Queries.GetById;
using Application.Features.Courts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Application.Features.Courts.Queries.GetListByUserId;
using Application.Features.Courts.Commands.UpdateActivity;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourtsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCourtResponse>> Add([FromBody] CreateCourtCommandDto createCourtCommandDto)
    {
        CreateCourtCommand command = new() { CreateCourtCommandDto = createCourtCommandDto, UserId = getUserIdFromRequest()};
        CreatedCourtResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedCourtResponse>> Update([FromBody] UpdateCourtCommandDto updateCourtCommandDto)
    {
        UpdateCourtCommand command = new() { UpdateCourtCommandDto = updateCourtCommandDto, UserId= getUserIdFromRequest()};
        UpdatedCourtResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCourtResponse>> Delete([FromRoute] Guid id)
    {
        DeleteCourtCommand command = new() { Id = id, UserId = getUserIdFromRequest() };

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

    [HttpGet("GetListByUserId")]
    public async Task<IActionResult> GetListByUserIdCourts([FromQuery] PageRequest pageRequest)
    {
        GetListByUserIdCourtQuery query = new() { PageRequest= pageRequest, UserId = getUserIdFromRequest() };
        GetListResponse<GetListByUserIdCourtListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPut("UpdateActivity")]
    public async Task<IActionResult> UpdateActivityCourt([FromBody] Guid id, bool isActive)
    {
        UpdateActivityCourtCommand command = new() { Id = id, IsActive = isActive, UserId = getUserIdFromRequest()};
        UpdatedActivityCourtResponse response = await Mediator.Send(command);
        return Ok(response);
    }
}