using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;
[ApiController]
[Route("api/admin/users")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles ="Admin")]
public class UserController(
IQueryHandler<GetUserByIdQuery, Result<GetUserDTO>> getByIdQueryHandler,
IQueryHandler<GetUsersQuery, PagedResult<List<GetUserDTO>>> getQueryHandler,
ICommandHandler<UpdateUserCommand, Result<string>> updateCommandHandler,
ICommandHandler<DeleteUserCommand, Result<string>> deleteCommandHandler
): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetItemsAsync([FromQuery] GetUsersQuery query)
    {
        var result = await getQueryHandler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var query= new GetUserByIdQuery(id);
        var result = await getByIdQueryHandler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
        
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateUserCommand command)
    {
        command.Id=id;  
        var result = await updateCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await deleteCommandHandler.HandleAsync(new DeleteUserCommand(id));

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }


    private IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NotFound => NotFound(new { error = result.Message }),
            ErrorType.Validation => BadRequest(new { error = result.Message }),
            ErrorType.Conflict => Conflict(new { error = result.Message }),
            ErrorType.Internal => StatusCode(500, new { error = result.Message }),
            _ => StatusCode(500, new { error = result.Message ?? "Unhandled error" })
        };
    }
}
