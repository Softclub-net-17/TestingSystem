using System;
using System.Runtime.InteropServices;

namespace Domain.Entities;

public class TestSession
{
    public int Id { get; set; }
    public int? TopicId { get; set; }
    public int SectionId{get;set;}
    public int  UserId { get; set; } 
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal ScorePercent { get; set; }              
    public int CorrectAnswersCount { get; set; }
    public int TotalQuestions { get; set; }
    public bool IsPassed { get; set; }

    public Topic Topic { get; set; } = null!;
    public User User{get;set;}=null!;
    public Section Section{get;set;}=null!;

}
