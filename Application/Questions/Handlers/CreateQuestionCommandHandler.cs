using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.Commands;
using Application.Questions.Mappers;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class CreateQuestionCommandHandler(
    ITopicRepository topicRepository,
    IQuestionRepository questionRepository,
    IValidator<CreateQuestionCommand> validator,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateQuestionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CreateQuestionCommand command)
    {
        var validationResult= validator.Validate(command);
        if (!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        
        var topicExists= await topicRepository.GetItemByIdAsync(command.TopicId);
        if(topicExists==null)
            return Result<string>.Fail($"Topic with given id : {command.TopicId} doesnt exist");
        
        var newQuestion=command.ToEntity();

        await questionRepository.Create(newQuestion);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null,"Question created successfully!");
    }
}