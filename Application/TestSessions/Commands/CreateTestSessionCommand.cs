using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.TestSessions.Commands;

public class CreateTestSessionCommand:ICommand<Result<string>>
{
    public int SectionId{get;set;}
    [JsonIgnore]
    public int UserId{get;set;}
}
