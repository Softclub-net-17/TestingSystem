using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Questions.Commands;

public class CreateQuestionCommand : ICommand<Result<string>>
{
    public int TopicId{get;set;}
    public string Text{get;set;}=null!;
}