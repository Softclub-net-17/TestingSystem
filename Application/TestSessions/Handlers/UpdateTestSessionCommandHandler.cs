using System;
using System.Globalization;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Commands;
using Application.TestSessions.Commands;
using Application.TestSessions.DTOs;
using Application.TestSessions.Mappers;
using Domain.Interfaces;

namespace Application.TestSessions.Handlers;

public class UpdateTestSessionCommandHandler(
IAnswerOptionRepository answerOptionRepository,
ITestSessionRepository testSessionRepository,
IUnitOfWork unitOfWork
): ICommandHandler<UpdateTestSessionCommand, Result<GetUpdateTestSessionResponseDto>>
{
    public async Task<Result<GetUpdateTestSessionResponseDto>> HandleAsync(UpdateTestSessionCommand command)
    {
        var exists= await testSessionRepository.GetItemByIdAsync(command.Id);
        if(exists==null)
            return Result<GetUpdateTestSessionResponseDto>.Fail($"Test session with given id: {command.Id} not found");

        var correctAnswersCount= await answerOptionRepository.CorrectAnswerCountAsync(command.ChoosedOptions);
        var getScoreAndIsPassed= GetScoreAndIsPassed(correctAnswersCount);
        command.MapFrom(exists,getScoreAndIsPassed.ScorePercent, correctAnswersCount,getScoreAndIsPassed.IsPassed);

        await unitOfWork.SaveChangesAsync();
        var response=exists.ToUpdatedDto();
    
        return Result<GetUpdateTestSessionResponseDto>.Ok(response);
    }


    private static (decimal ScorePercent,bool IsPassed) GetScoreAndIsPassed(int coreectAnswers)
    {
        var isPassed=false;
        decimal percent=coreectAnswers*10;
        if(percent>=80) isPassed=true;

        return (percent,isPassed); 
    }
}
