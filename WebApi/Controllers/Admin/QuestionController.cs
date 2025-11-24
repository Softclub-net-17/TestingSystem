using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.Commands;
using Application.Questions.DTOs;
using Application.Questions.Handlers;
using Application.Questions.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[ApiController]
[Route("api/admin/questions")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles ="Admin")]

public class QuestionController(
    ICommandHandler<CreateQuestionCommand, Result<string>> createQuestionCommandHandler,
    ICommandHandler<UpdateQuestionCommand, Result<string>> updateQuestionCommandHandler,
    ICommandHandler<ChangeQuestionStatusCommand, Result<string>> changeQuestionStatusCommandHandler,
    IQueryHandler<GetQuestionsByTopicIdQuery, Result<List<GetQuestionDto>>> getQuestionsByTopicIdQueryHandler,
    IQueryHandler<GetQuestionByIdQuery, Result<GetQuestionDto>> getQuestionByIdQueryHandler,
    IQueryHandler<GetQuestionsQuery, Result<List<GetQuestionDto>>> getQuestionsQueryHandler)
        : ControllerBase

{
    [HttpGet]
    public async Task<IActionResult> GetItemsAsync()
    {
        var result = await getQuestionsQueryHandler.HandleAsync(new GetQuestionsQuery());

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    [HttpGet("by-topicId/{id:int}")]
    public async Task<IActionResult> GetItemsByTopicIdAsync(int id)
    {
        var result = await getQuestionsByTopicIdQueryHandler.HandleAsync(new GetQuestionsByTopicIdQuery(id));

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result = await getQuestionByIdQueryHandler.HandleAsync(new GetQuestionByIdQuery(id));

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItemAsync(CreateQuestionCommand command)
    {
        var result = await createQuestionCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateItemAsync(int id, UpdateQuestionCommand command)
    {
        command.Id = id;
        var result = await updateQuestionCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }

    [HttpPatch("change-status/{id:int}")]
    public async Task<IActionResult> ChangeStatusAsync(int id, [FromQuery] bool status)
    {
        var result = await changeQuestionStatusCommandHandler.HandleAsync(new ChangeQuestionStatusCommand(id, status));

        if (!result.IsSuccess)
            return HandleError(result);

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