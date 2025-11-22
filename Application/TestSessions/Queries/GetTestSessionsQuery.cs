using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.DTOs;

namespace Application.TestSessions.Queries;

public class GetTestSessionsQuery:IQuery<PagedResult<List<GetTestSessionDto>>>
{
    [JsonIgnore]
    public int? TopicId { get; set; }
    public int? SectionId{get;set;}
    public int?  UserId { get; set; } 
    public DateOnly? DueDate { get; set; }
    public decimal? ScorePercent { get; set; }   
    public bool? IsPassed { get; set; }
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
