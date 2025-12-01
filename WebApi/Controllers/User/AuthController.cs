using System;
using System.Security.Claims;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Common.Results;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User;
[ApiController]
[Route("api/auth")]
[ApiExplorerSettings(GroupName = "client")]


public class AuthController(
    ICommandHandler<LoginCommand, Result<AuthResponseDto>> loginCommandHandler,
    ICommandHandler<RegisterCommand, Result<string>> registerCommandHandler,
    ICommandHandler<ChangePasswordCommand, Result<string>> changePasswordCommandHandler,
    ICommandHandler<RequestResetPasswordCommand, Result<string>> requestResetPasswordCommandHandler,
    ICommandHandler<VerifyCodeCommand, Result<string>> verifyCodeCommandHandler,
    ICommandHandler<ResetPasswordCommand, Result<string>> resetPasswordCommandHandler,
    ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>> refreshTokenHandler)
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
        
        // refreshToken кладём в cookie
        Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        // возвращаем только accessToken
        return Ok(new { accessToken = result.Data!.AccessToken });
    }
    
    [Authorize(Roles ="User")]
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
    
    [HttpPost("request-reset-password")]
    public async Task<IActionResult> RequestResetPasswordAsync(RequestResetPasswordCommand command)
    {
        var result = await requestResetPasswordCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result.Message);
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCodeAsync(VerifyCodeCommand command)
    {
        var result = await verifyCodeCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result.Message);
    }

     
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand command)
    {
        var result = await resetPasswordCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result.Message);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync()
    {
        // 1. Достаём refresh‑token из cookie
        var token = Request.Cookies["refreshToken"];
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized(new { error = "Refresh token is missing." });
        }

        // 2. Создаём команду
        var command = new RefreshTokenCommand(token);

        // 3. Вызываем хендлер
        var result = await refreshTokenHandler.HandleAsync(command);

        // 4. Обрабатываем ошибку
        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        // 5. Кладём новый refresh‑token в cookie
        Response.Cookies.Append("refreshToken", command.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires =  DateTime.UtcNow.AddDays(7)
        });

        // 6. Возвращаем новый access‑token
        return Ok(new { accessToken = result.Data.AccessToken });
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
