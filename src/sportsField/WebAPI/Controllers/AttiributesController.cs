using Application.Features.Attiributes.Commands.Create;
using Application.Features.Attiributes.Commands.Delete;
using Application.Features.Attiributes.Commands.Update;
using Application.Features.Attiributes.Queries.GetById;
using Application.Features.Attiributes.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Persistence.Dynamic;
using Application.Features.Attiributes.Queries.GetListByDynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttiributesController : BaseController
{
    //[HttpPost]
    //public async Task<ActionResult<CreatedAttiributeResponse>> Add([FromBody] CreateAttiributeCommand command)
    //{
    //    CreatedAttiributeResponse response = await Mediator.Send(command);

    //    return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    //}

    //[HttpPut]
    //public async Task<ActionResult<UpdatedAttiributeResponse>> Update([FromBody] UpdateAttiributeCommand command)
    //{
    //    UpdatedAttiributeResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<DeletedAttiributeResponse>> Delete([FromRoute] int id)
    //{
    //    DeleteAttiributeCommand command = new() { Id = id };

    //    DeletedAttiributeResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAttiributeResponse>> GetById([FromRoute] int id)
    {
        GetByIdAttiributeQuery query = new() { Id = id };

        GetByIdAttiributeResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListAttiributeQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAttiributeQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListAttiributeListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamicAttiribute([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamicQuery)
    {
        GetListByDynamicAttiributeQuery query = new() { DynamicQuery = dynamicQuery , PageRequest = pageRequest};
        GetListResponse<GetListByDynamicAttiributeListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}