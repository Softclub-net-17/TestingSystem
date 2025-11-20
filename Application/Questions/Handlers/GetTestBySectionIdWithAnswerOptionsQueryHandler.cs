using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetTestBySectionIdWithAnswerOptionsQueryHandler(
    IQuestionRepository questionRepository,
    IAnswerOptionRepository answerOptionRepository
) : IQueryHandler<GetTestBySectionIdWithAnswerOptionsQuery, Result<List<GetQuestionWithOptionsDto>>>
{
    public async Task<Result<List<GetQuestionWithOptionsDto>>> HandleAsync(GetTestBySectionIdWithAnswerOptionsQuery query)
    {
        var questions = await questionRepository.GetRandomedTestsBySectionIdAsync(query.SectionId);

        var questionAndOptionsList = new List<GetQuestionWithOptionsDto>();
        foreach (var q in questions)
        {
            var answerOptions = await answerOptionRepository.GetRandomedAnswerOptionsByQuestionIdAsync(q.Id);

            questionAndOptionsList.Add(q.ToWithOptionsDto(answerOptions));
        }

        return Result<List<GetQuestionWithOptionsDto>>.Ok(questionAndOptionsList);
    }
}