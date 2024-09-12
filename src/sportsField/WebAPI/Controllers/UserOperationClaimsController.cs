﻿using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.CreateAccess;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    //[HttpGet("{Id}")]
    //public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimQuery getByIdUserOperationClaimQuery)
    //{
    //    GetByIdUserOperationClaimResponse result = await Mediator.Send(getByIdUserOperationClaimQuery);
    //    return Ok(result);
    //}

    //[HttpGet]
    //public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    //{
    //    GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };
    //    GetListResponse<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserOperationClaimQuery);
    //    return Ok(result);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    //{
    //    CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand);
    //    return Created(uri: "", result);
    //}

    //[HttpPut]
    //public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    //{
    //    UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);
    //    return Ok(result);
    //}

    //[HttpDelete]
    //public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
    //{
    //    DeletedUserOperationClaimResponse result = await Mediator.Send(deleteUserOperationClaimCommand);
    //    return Ok(result);
    //}

    [HttpPost("CAC")]
    public async Task<IActionResult> CreateAccess([FromBody] CreateAccessCommandDto createAccessCommandDto)
    {
        CreateAccessCommand command = new() {CreateAccessDto = createAccessCommandDto};
        bool result = await Mediator.Send(command);
        return Ok(result);
    }
}
