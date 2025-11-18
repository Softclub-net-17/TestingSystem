using System;
using Application.Interfaces;
using Application.Sections.Commands;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Results;
using Application.Sections.Queries;
using Application.Sections.DTOs;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;
[ApiController]
[Route("api/sections")]
public class SectionController(
ICommandHandler<CreateSectionCommand, Result<string>> createCommandHandler,
ICommandHandler<ChangeSectionStatusCommand, Result<string>> changestatusCommandHandler,
ICommandHandler<UpdateSectionCommand, Result<string>> updateCommandHandler,
IQueryHandler<GetSectionsQuery,PagedResult<List<GetSectionDTO>>> getQueryHandler,
IQueryHandler<GetSectionByIdQuery, Result<GetSectionDTO>> getByIdQyeryHandler,
IQueryHandler<GetActiveSectionsQuery, Result<List<GetSectionDTO>>> getActiveQueryHandler
):ControllerBase
{
    [Authorize(Roles ="Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetItemsAsync([FromQuery] GetSectionsQuery query)
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
        var result = await getActiveQueryHandler.HandleAsync(new GetActiveSectionsQuery());

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
        var result = await getByIdQyeryHandler.HandleAsync(new GetSectionByIdQuery(id));

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    [Authorize(Roles ="Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateItemAsync(CreateSectionCommand command)
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
    public async Task<IActionResult> UpdateItemAsync(int id, UpdateSectionCommand command)
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
        var result =await changestatusCommandHandler.HandleAsync(new ChangeSectionStatusCommand(id, status));

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
