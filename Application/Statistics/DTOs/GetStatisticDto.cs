namespace Application.Statistics.DTOs;

public class GetStatisticDto
{
    public int CountActiveSections { get; set; }
    public int TotalQuestions { get; set; }
    public int CompletionRate { get; set; } 
    public int ActiveStudents { get; set; }
}