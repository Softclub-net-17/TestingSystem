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

namespace WebApi.Controllers;
[ApiController]
[Route("api/topics")]
public class TopicController(
ICommandHandler<CreateTopicCommand, Result<string>> createCommandHandler,
ICommandHandler<ChangeTopicStatusCommand, Result<string>> changestatusCommandHandler,
ICommandHandler<UpdateTopicCommand, Result<string>> updateCommandHandler,
IQueryHandler<GetTopicsQuery,PagedResult<List<GetTopicDto>>> getQueryHandler,
IQueryHandler<GetActiveTopicsQuery,Result<List<GetTopicDto>>> getActiveQueryHandler,
IQueryHandler<GetTopicsBySectionIdQuery,Result<List<GetTopicDto>>> getBySectionIdQueryHandler,
IQueryHandler<GetTopicByIdQuery, Result<GetTopicDto>> getByIdQyeryHandler
):ControllerBase
{
    [Authorize(Roles ="Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetItemsAsync([FromQuery] GetTopicsQuery query)
    {
        var result = await getQueryHandler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    [Authorize(Roles ="User")]

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveItemsAsync()
    {
        var result = await getActiveQueryHandler.HandleAsync(new GetActiveTopicsQuery());

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    [Authorize(Roles ="Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result = await getByIdQyeryHandler.HandleAsync(new GetTopicByIdQuery(id));

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    [Authorize(Roles ="Admin")]
    [HttpGet("-by-sectionId-{id:int}")]
    public async Task<IActionResult> GetItemBySectionIdAsync(int id)
    {
        var result = await getBySectionIdQueryHandler.HandleAsync(new GetTopicsBySectionIdQuery(id));

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }

    [Authorize(Roles ="Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateItemAsync(CreateTopicCommand command)
    {
        var result= await createCommandHandler.HandleAsync(command);
        
        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    [Authorize(Roles ="Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateItemAsync(int id, UpdateTopicCommand command)
    {
        command.Id=id;
        var result = await updateCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    [Authorize(Roles ="Admin")]
    [HttpPatch("{id:int}/change-status")]
    public async Task<IActionResult> ChangeStatusAsync(int id,[FromQuery] bool status)
    {
        var result =await changestatusCommandHandler.HandleAsync(new ChangeTopicStatusCommand(id, status));

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
