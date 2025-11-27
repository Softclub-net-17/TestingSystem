using System;
using System.Security.Claims;
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User;
[ApiController]
[Route("api/auth")]
[ApiExplorerSettings(GroupName = "client")]


public class AuthController(
    ICommandHandler<LoginCommand, Result<string>> loginCommandHandler,
    ICommandHandler<RegisterCommand, Result<string>> registerCommandHandler,
    ICommandHandler<ChangePasswordCommand, Result<string>> changePasswordCommandHandler)
        : ControllerBase
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
    
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            return BadRequest(new { error = "Invalid userId in token" });

        command.UserId = userId;

        var result = await changePasswordCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(new { message = "Password changed successfully" });
    }


    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterCommand command)
    {
        var result = await registerCommandHandler.HandleAsync(command);

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
