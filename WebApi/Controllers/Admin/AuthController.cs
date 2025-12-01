using System;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[ApiController]
[Route("api/admin/auth")]
[ApiExplorerSettings(GroupName = "admin")]

public class AuthController(
    ICommandHandler<LoginCommand, Result<AuthResponseDto>> loginCommandHandler,
    ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>> refreshTokenHandler):ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var result = await loginCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        
        return Ok(result.Data);
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
            Expires = DateTime.UtcNow.AddDays(7)
        });

        // 6. Возвращаем новый access‑token
        return Ok(new { accessToken = result.Data!.AccessToken });
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
