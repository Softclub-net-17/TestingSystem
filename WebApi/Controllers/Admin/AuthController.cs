using System;
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[ApiController]
[Route("api/admin/auth")]
[ApiExplorerSettings(GroupName = "admin")]

public class AuthController(ICommandHandler<LoginCommand, Result<string>> loginCommandHandler):ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var result = await loginCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
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
