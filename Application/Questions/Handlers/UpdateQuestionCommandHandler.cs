using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.Commands;
using Application.Questions.Mappers;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class UpdateQuestionCommandHandler(
    IQuestionRepository questionRepository,
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateQuestionCommand> validator)
    : ICommandHandler<UpdateQuestionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateQuestionCommand command)
    {
        var validationResult= validator.Validate(command);
        
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        
        var exists=await questionRepository.GetByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail("Topic to update doesnt exist");
       
        var topicExists= await topicRepository.GetByIdItemAsync(command.TopicId);
        if(topicExists==null)
            return Result<string>.Fail($"Topic with given id : {command.TopicId} doesnt exist");
        if(!topicExists.IsPublished)
            return Result<string>.Fail("Cannot create question to unpublished topic",ErrorType.Conflict);

        var noChange = exists.TopicId == command.TopicId
                       && exists.Text.Equals(command.Text.Trim(), StringComparison.CurrentCultureIgnoreCase);
        if(noChange)
            return Result<string>.Fail("No changes were made",ErrorType.NoChange);
        
        command.MapFrom(exists);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Question updated successfully!");

    }
}