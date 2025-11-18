using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.Commands;
using Application.Questions.Mappers;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class ChangeQuestionStatusCommandHandler(
    IQuestionRepository  questionRepository,
    ITopicRepository  topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ChangeQuestionStatusCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ChangeQuestionStatusCommand command)
    {
        var exists = await questionRepository.GetByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail($"There is no such question by {command.Id} id", ErrorType.NotFound);

        if (command.Status)
        {
            var activeTopic = await topicRepository.GetByIdItemAsync(exists.TopicId);
            if(!activeTopic!.IsPublished)
                return Result<string>.Fail("Cannot change the question of unpublished topic", ErrorType.Conflict);
            
        }
        
        exists.ChangeStatus(command.Status);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Status changed successfully!");
    }
}