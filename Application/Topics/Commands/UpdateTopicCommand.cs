using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Topics.Commands;

public class UpdateTopicCommand:ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id{get;set;}
    public int SectionId{get;set;}
    public string Title{get;set;}=string.Empty;
    public string Content{get;set;}=string.Empty;
    public bool IsPublished{get;set;}
}
