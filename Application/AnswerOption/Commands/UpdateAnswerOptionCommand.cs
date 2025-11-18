using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.AnswerOption.Commands;

public class UpdateAnswerOptionCommand : ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id{get;set;}
    public int QuestionId{get;set;}
    public string Text{get;set;}=null!;
    public bool IsCorrect {get;set;}
}