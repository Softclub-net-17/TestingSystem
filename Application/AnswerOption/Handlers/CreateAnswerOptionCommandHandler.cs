using Application.AnswerOption.Commands;
using Application.AnswerOption.Mappers;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.AnswerOption.Handlers;

public class CreateAnswerOptionCommandHandler(
    IQuestionRepository questionRepository,
    IAnswerOptionRepository answerOptionRepository,
    IValidator<CreateAnswerOptionCommand> validator,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateAnswerOptionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CreateAnswerOptionCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result<string>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        }

        var questionExists = await questionRepository.GetByIdAsync(command.QuestionId);
        if (questionExists == null)
            return Result<string>.Fail($"Question with given id : {command.QuestionId} doesn't exist");

        var newAnswerOption = command.ToEntity();

        await answerOptionRepository.CreateAsync(newAnswerOption);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Answer option created successfully!");
    }
}