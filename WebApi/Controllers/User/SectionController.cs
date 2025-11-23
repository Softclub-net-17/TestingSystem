using System;
using Application.Interfaces;
using Application.Sections.Commands;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Results;
using Application.Questions.DTOs;
using Application.Questions.Queries;
using Application.Sections.Queries;
using Application.Sections.DTOs;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.User;
[ApiController]
[Route("api/sections")]
[ApiExplorerSettings(GroupName = "client")]
[Authorize(Roles ="User")]

public class SectionController(
IQueryHandler<GetActiveSectionsQuery, PagedResult<List<GetSectionDTO>>> getActiveQueryHandler,
IQueryHandler<GetTestBySectionIdWithAnswerOptionsQuery, Result<List<GetQuestionWithOptionsDto>>> getTestBySectionIdWithAnswerOptionsQueryHandler
):ControllerBase
{

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveItemsAsync([FromQuery] GetActiveSectionsQuery query)
    {
        var result = await getActiveQueryHandler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }

    [HttpGet("test/{sectionId:int}")]
    public async Task<IActionResult> GetTestBySectionIdAsync(int sectionId)
    {
        var result = await getTestBySectionIdWithAnswerOptionsQueryHandler
            .HandleAsync(new GetTestBySectionIdWithAnswerOptionsQuery(sectionId));

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
