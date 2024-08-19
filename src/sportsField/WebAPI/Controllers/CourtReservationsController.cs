using Application.Features.CourtReservations.Commands.Create;
using Application.Features.CourtReservations.Commands.Delete;
using Application.Features.CourtReservations.Commands.Update;
using Application.Features.CourtReservations.Queries.GetById;
using Application.Features.CourtReservations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Application.Features.CourtReservations.Commands.QuickCreate;
using Application.Features.CourtReservations.Commands.UpdateActivity;
using Application.Features.CourtReservations.Queries.GetListById;
using Domain.Entities;
using Application.Features.CourtReservations.Commands.RentReservation;
using Application.Features.CourtReservations.Queries.GetListByUserId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourtReservationsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCourtReservationResponse>> Add([FromBody] CreateCourtReservationCommandDto createCourtReservationCommandDto)
    {
        CreateCourtReservationCommand command = new() { CreateCourtReservationCommandDto = createCourtReservationCommandDto, UserId = getUserIdFromRequest() };
        CreatedCourtReservationResponse response = await Mediator.Send(command);

        return Ok(response);
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
        DeleteCourtReservationCommand command = new() { Id = id , UserId = getUserIdFromRequest()};

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

    [HttpPost("QuickCreate")]
    public async Task<IActionResult> QuickCreateCourtReservation([FromBody] QuickCreateCourtReservationCommandDto quickCreateCourtReservationCommandDto)
    {
        QuickCreateCourtReservationCommand command = new() { QuickCreateCourtReservationCommandDto = quickCreateCourtReservationCommandDto, UserId = getUserIdFromRequest() };
        QuickCreatedCourtReservationResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("UpdateActivity")]
    public async Task<IActionResult> UpdateActivityCourtReservation([FromBody] Guid id, bool isActive)
    {
        UpdateActivityCourtReservationCommand command = new() { Id = id, IsActive = isActive, UserId = getUserIdFromRequest() };
        UpdatedActivityCourtReservationResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("GetListByCourtId/{courtId}")]
    public async Task<IActionResult> GetListByIdCourtReservation([FromRoute] Guid courtId)
    {
        GetListByIdCourtReservationQuery query = new() { CourtId = courtId};
        ICollection<CourtReservation> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("RentReservation")]
    public async Task<IActionResult> RentReservation([FromBody] Guid id)
    {
        RentReservationCommand command = new() { Id= id , UserId = getUserIdFromRequest()};
        RentReservationResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("GetListByUserId")]
    public async Task<IActionResult> GetListByUserIdCourtReservation([FromQuery] PageRequest pageRequest)
    {
        GetListByUserIdCourtReservationQuery command = new() { PageRequest = pageRequest , UserId = getUserIdFromRequest()};
        GetListResponse<GetListByUserIdCourtReservationListItemDto> response = await Mediator.Send(command);
        return Ok(response);
    }
}