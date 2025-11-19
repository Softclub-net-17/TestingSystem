using Application.AnswerOption.Commands;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.AnswerOption.Handlers;

public class DeleteAnswerOptionCommandHandler(
    IAnswerOptionRepository answerOptionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteAnswerOptionCommand, Result<string>>

{
    public async Task<Result<string>> HandleAsync(DeleteAnswerOptionCommand command)
    {
        var exists = await answerOptionRepository.GetItemByIdAsync(command.Id);
        
        if(exists == null)
            return Result<string>.Fail("Answer option not found", ErrorType.NotFound);
        
        await answerOptionRepository.DeleteAsync(exists);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Answer option deleted successfully");
    }
}