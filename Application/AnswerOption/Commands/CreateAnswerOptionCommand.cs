using Application.Common.Results;
using Application.Interfaces;

namespace Application.AnswerOption.Commands;

public class CreateAnswerOptionCommand : ICommand<Result<string>>
{
    public int QuestionId{get;set;}
    public string Text{get;set;}=null!;
    public bool IsCorrect {get;set;}
}