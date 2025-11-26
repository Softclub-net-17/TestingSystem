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

namespace WebApi.Controllers.Admin;
[ApiController]
[Route("api/admin/topics")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles ="Admin")]
public class TopicController(
ICommandHandler<CreateTopicCommand, Result<string>> createCommandHandler,
ICommandHandler<ChangeTopicStatusCommand, Result<string>> changestatusCommandHandler,
ICommandHandler<UpdateTopicCommand, Result<string>> updateCommandHandler,
IQueryHandler<GetTopicsQuery,PagedResult<List<GetTopicDto>>> getQueryHandler,
IQueryHandler<GetTopicsBySectionIdQuery,Result<List<GetTopicBySectionIdDto>>> getBySectionIdQueryHandler,
IQueryHandler<GetTopicByIdQuery, Result<GetTopicDto>> getByIdQyeryHandler
):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetItemsAsync([FromQuery] GetTopicsQuery query)
    {
        var result = await getQueryHandler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
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
    [HttpGet("/by-sectionId-{id:int}")]
    public async Task<IActionResult> GetItemBySectionIdAsync(int id)
    {
        var result = await getBySectionIdQueryHandler.HandleAsync(new GetTopicsBySectionIdQuery(id));

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
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
    [HttpPatch("change-status/{id:int}")]
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
