using System;
using System.Globalization;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Commands;
using Application.TestSessions.Commands;
using Application.TestSessions.Mappers;
using Domain.Interfaces;

namespace Application.TestSessions.Handlers;

public class UpdateTestSessionCommandHandler(
IAnswerOptionRepository answerOptionRepository,
ITestSessionRepository testSessionRepository,
IUnitOfWork unitOfWork,
IValidator<UpdateTestSessionCommand> validator
): ICommandHandler<UpdateTestSessionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateTestSessionCommand command)
    {
        var validationResult=validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);

        var exists= await testSessionRepository.GetItemByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail($"Test session with given id: {command.Id} not found");

        var correctAnswersCount= await answerOptionRepository.CorrectAnswerCountAsync(command.ChoosedOptions);
        var getScoreAndIsPassed= GetScoreAndIsPassed(correctAnswersCount);
        command.MapFrom(exists,getScoreAndIsPassed.ScorePercent, correctAnswersCount,getScoreAndIsPassed.IsPassed);

        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Test session updated successfully!");
    }


    private static (decimal ScorePercent,bool IsPassed) GetScoreAndIsPassed(int coreectAnswers)
    {
        var isPassed=false;
        decimal percent=coreectAnswers*10;
        if(percent<=80) isPassed=true;

        return (percent,isPassed); 
    }
}
