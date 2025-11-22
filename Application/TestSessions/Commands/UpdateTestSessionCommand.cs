using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.TestSessions.Commands;

public class UpdateTestSessionCommand: ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public List<int> ChoosedOptions {get;set;}=[];
    [JsonIgnore]
    public decimal ScorePercent { get; set; }  
    [JsonIgnore]
    public int CorrectAnswersCount { get; set; }
    [JsonIgnore]
    public bool IsPassed { get; set; }
}
