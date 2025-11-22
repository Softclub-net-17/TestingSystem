using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.TestSessions.Commands;

public class CreateTestSessionCommand:ICommand<Result<string>>
{
    public int SectionId{get;set;}
    public int  UserId { get; set; } 
}
