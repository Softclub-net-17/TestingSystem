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
        {
            return Result<string>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        }

        var topicExists= await topicRepository.GetByIdItemAsync(command.TopicId);
        if(topicExists==null)
            return Result<string>.Fail($"Section with given id : {command.TopicId} doesnt exist");
        
        if(!topicExists.IsPublished)
            return Result<string>.Fail("Cannot create topic to inactive section",ErrorType.Conflict);
        
        
        var newQuestion=command.ToEntity();
        await questionRepository.Create(newQuestion);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null,"Topic created successfully!");
    }
}