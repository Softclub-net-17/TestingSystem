using Application.AnswerOption.Commands;
using Application.AnswerOption.DTOs;
using Application.AnswerOption.Queries;
using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/answerOption")]
public class AnswerOptionController(
    ICommandHandler<CreateAnswerOptionCommand, Result<string>> createAnswerOptionCommandHandler,
    ICommandHandler<UpdateAnswerOptionCommand, Result<string>> updateAnswerOptionCommandHandler,
    ICommandHandler<DeleteAnswerOptionCommand, Result<string>> deleteAnswerOptionCommandHandler,
    IQueryHandler<GetAnswerOptionByIdQuery, Result<GetAnswerOptionDto>> getAnswerOptionByIdQueryHandler,
    IQueryHandler<GetAnswerOptionByQuestionIdQuery, Result<List<GetAnswerOptionDto>>> getAnswerOptionsByQuestionIdQueryHandler)
: ControllerBase   
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result = await getAnswerOptionByIdQueryHandler.HandleAsync(new GetAnswerOptionByIdQuery (id ));

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "User")]
    [HttpGet("by-question/{questionId:int}")]
    public async Task<IActionResult> GetItemsByQuestionIdAsync(int questionId)
    {
        var result = await getAnswerOptionsByQuestionIdQueryHandler.HandleAsync(
            new GetAnswerOptionByQuestionIdQuery (questionId ));

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateItemAsync(CreateAnswerOptionCommand command)
    {
        var result = await createAnswerOptionCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateItemAsync(int id, UpdateAnswerOptionCommand command)
    {
        command.Id = id;
        var result = await updateAnswerOptionCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteItemAsync(int id)
    {
        var result = await deleteAnswerOptionCommandHandler.HandleAsync(new DeleteAnswerOptionCommand(id));

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