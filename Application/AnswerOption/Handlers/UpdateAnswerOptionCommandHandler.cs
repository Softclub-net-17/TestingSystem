using Application.AnswerOption.Commands;
using Application.AnswerOption.Mappers;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.AnswerOption.Handlers;

public class UpdateAnswerOptionCommandHandler(
    IAnswerOptionRepository answerOptionRepository,
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateAnswerOptionCommand> validator)
    : ICommandHandler<UpdateAnswerOptionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateAnswerOptionCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);

        var exists = await answerOptionRepository.GetItemByIdAsync(command.Id);
        if (exists == null)
            return Result<string>.Fail("Answer option to update doesn't exist", ErrorType.NotFound);

        var questionExists = await questionRepository.GetByIdAsync(command.QuestionId);
        if (questionExists == null)
            return Result<string>.Fail("Question doesn't exist", ErrorType.NotFound);
        
        var noChange = exists.QuestionId == command.QuestionId && exists.Text.Equals(command.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)
                       && exists.IsCorrect == command.IsCorrect;
        if (noChange)
            return Result<string>.Fail("No changes were made", ErrorType.NoChange);

        command.MapFrom(exists);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Answer option updated successfully!");
    }
}