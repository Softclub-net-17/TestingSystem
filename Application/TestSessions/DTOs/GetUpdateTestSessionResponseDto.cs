using System;

namespace Application.TestSessions.DTOs;

public class GetUpdateTestSessionResponseDto
{
    public int Id { get; set; }
    public int SectionId{get;set;}
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal ScorePercent { get; set; }              
    public int CorrectAnswersCount { get; set; }
    public int TotalQuestions { get; set; }
    public bool IsPassed { get; set; }
}
