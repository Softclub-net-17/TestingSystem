using Application.Common.Results;
using Application.Interfaces;
using Application.Statistics.DTOs;
using Application.Statistics.Mappers;
using Application.Statistics.Queries;
using Domain.Interfaces;

namespace Application.Statistics.Handlers;

public class GetStatisticQueryHandler(
    IQuestionRepository questionRepository,
    ISectionRepository sectionRepository,
    IUserRepository userRepository,
    ITestSessionRepository testSessionRepository)
        : IQueryHandler<GetStatisticQuery, Result<GetStatisticDto>>
{
    public async Task<Result<GetStatisticDto>> HandleAsync(GetStatisticQuery query)
    {
        var countActiveSections = await sectionRepository.CountActiveAsync();
        
        var getAllQuestions = await questionRepository.CountAllQuestionsAsync();
        
        var completionRateAvg = await testSessionRepository.GetAverageScorePercentAsync();
        var completionRate = (int) Math.Round(completionRateAvg);

        var activeStudents = await userRepository.CountActiveItemsAsync();
        
        var dto = StatisticMapper.Map(
            countActiveSections,
            getAllQuestions,
            completionRate,
            activeStudents
        );

        return Result<GetStatisticDto>.Ok(dto, null);

    }
}