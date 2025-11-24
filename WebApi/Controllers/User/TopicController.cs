using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Queries;
using Application.Topics.Commands;
using Application.Topics.DTOs;
using Application.Topics.Handlers;
using Application.Topics.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User;
[ApiController]
[Route("api/topics")]
[Authorize(Roles = "User")]
[ApiExplorerSettings(GroupName = "client")]
public class TopicController(
IQueryHandler<GetActiveTopicsQuery,PagedResult<List<GetTopicDto>>> getActiveQueryHandler
):ControllerBase
{

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveItemsAsync([FromQuery] GetActiveTopicsQuery query)
    {
        var result = await getActiveQueryHandler.HandleAsync(query);

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
