using System;
using System.Security.Claims;
using System.Windows.Input;
using Application.AnswerOption.Queries;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.Commands;
using Application.TestSessions.DTOs;
using Application.TestSessions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/test-session")]
public class TestSessionController(
    ICommandHandler<CreateTestSessionCommand, Result<string>>  createCommandHandler,
    ICommandHandler<UpdateTestSessionCommand, Result<GetUpdateTestSessionResponseDto>> updateCommandHandler,
    IQueryHandler<GetTestSessionsQuery, PagedResult<List<GetTestSessionDto>>> getQueryHandler,
    IQueryHandler<GetTestSessionByIdQuery, Result<GetTestSessionDto>> getByIdQeuryHandler
):ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetItemsAsync([FromQuery] GetTestSessionsQuery query)
    {
        var result= await getQueryHandler.HandleAsync(query);
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result= await getByIdQeuryHandler.HandleAsync(new GetTestSessionByIdQuery(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateItemAsync(CreateTestSessionCommand command)
    {

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            return BadRequest(new { error = "Invalid userId in token" });
        command.UserId=userId;
        var result= await createCommandHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }
    [Authorize(Roles = "User")]
    [HttpPut("{id:int}")]

    public async Task<IActionResult> UpdateItemAsync(int id,UpdateTestSessionCommand command)
    {
        command.Id=id;
        var result= await updateCommandHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
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
