using Application.Features.Retentions.Commands.Create;
using Application.Features.Retentions.Commands.Delete;
using Application.Features.Retentions.Commands.Update;
using Application.Features.Retentions.Queries.GetById;
using Application.Features.Retentions.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Application.Features.Retentions.Queries.GetListByUserId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RetentionsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedRetentionResponse>> Add([FromBody] CreateRetentionCommandDto createRetentionCommandDto)
    {
        CreateRetentionCommand command = new() { CreateRetentionCommandDto = createRetentionCommandDto, UserId = getUserIdFromRequest()};
        CreatedRetentionResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedRetentionResponse>> Delete([FromRoute] Guid id)
    {
        DeleteRetentionCommand command = new() { Id = id };

        DeletedRetentionResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdRetentionResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdRetentionQuery query = new() { Id = id };

        GetByIdRetentionResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListRetentionQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRetentionQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListRetentionListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("GetListByUserId")]
    public async Task<IActionResult> GetListByUserIdRetention()
    {
        GetListByUserIdRetentionQuery query = new() { UserId = getUserIdFromRequest()};
        ICollection<GetListByUserIdRetentionListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}