using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.DTOs;

namespace Application.TestSessions.Commands;

public class UpdateTestSessionCommand: ICommand<Result<GetUpdateTestSessionResponseDto>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public List<int> ChoosedOptions {get;set;}=[];
    
}
