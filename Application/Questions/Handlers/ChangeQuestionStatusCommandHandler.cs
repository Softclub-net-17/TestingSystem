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
        
        if(exists.IsActive == command.IsActive)
            return Result<string>.Fail("You didn't change the status", ErrorType.Validation);
        
        exists.ChangeStatus(command.IsActive);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"IsActive changed successfully!");
    }
}