using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Topics.Commands;

public class CreateTopicCommand:ICommand<Result<string>>
{
    public int SectionId{get;set;}
    public string Title{get;set;}=null!;
    public string Content{get;set;}=string.Empty;
}