using System.Security.Claims;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.DTOs;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User;

[ApiController]
[Route("api/me")]
[Authorize]
[ApiExplorerSettings(GroupName = "client")]

public class MeController(
    IQueryHandler<GetUserByIdQuery, Result<GetUserDTO>> getUserByIdQueryHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMyInfoAsync()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!int.TryParse(userIdClaim, out var userId))
            return BadRequest(new { error = "Invalid userId in token" });
        
        var result = await getUserByIdQueryHandler.HandleAsync(new GetUserByIdQuery(userId));
        
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