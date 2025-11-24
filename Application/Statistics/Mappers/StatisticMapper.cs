using Application.Statistics.DTOs;

namespace Application.Statistics.Mappers;

public class StatisticMapper
{
    public static GetStatisticDto Map(
        int countActiveSections,
        int totalQuestions,
        decimal completionRate,
        int activeStudents)
    {
        return new GetStatisticDto
        {
            CountActiveSections = countActiveSections,
            TotalQuestions = totalQuestions,
            CompletionRate = (int)Math.Round(completionRate),
            ActiveStudents = activeStudents
        };
    }
}