using Application.Features.CourtReservations.Commands.Create;
using Application.Features.CourtReservations.Commands.Delete;
using Application.Features.CourtReservations.Commands.Update;
using Application.Features.CourtReservations.Queries.GetById;
using Application.Features.CourtReservations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourtReservationsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCourtReservationResponse>> Add([FromBody] CreateCourtReservationCommand command)
    {
        CreatedCourtReservationResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedCourtReservationResponse>> Update([FromBody] UpdateCourtReservationCommand command)
    {
        UpdatedCourtReservationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCourtReservationResponse>> Delete([FromRoute] Guid id)
    {
        DeleteCourtReservationCommand command = new() { Id = id };

        DeletedCourtReservationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCourtReservationResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdCourtReservationQuery query = new() { Id = id };

        GetByIdCourtReservationResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListCourtReservationQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCourtReservationQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListCourtReservationListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}