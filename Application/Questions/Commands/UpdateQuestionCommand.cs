using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Questions.Commands;

public class UpdateQuestionCommand :  ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id{get;set;}
    public int TopicId{get;set;}
    public string Text{get;set;}=string.Empty;
}