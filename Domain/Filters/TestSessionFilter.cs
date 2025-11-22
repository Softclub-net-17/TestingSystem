using System;

namespace Domain.Filters;

public class TestSessionFilter
{
    public int? TopicId { get; set; }
    public int? SectionId{get;set;}
    public int?  UserId { get; set; } 
    public DateOnly? DueDate { get; set; }
    public decimal? ScorePercent { get; set; }   
    public bool? IsPassed { get; set; }
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
