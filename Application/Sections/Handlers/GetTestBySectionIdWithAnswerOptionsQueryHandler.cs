using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class GetTestBySectionIdWithAnswerOptionsQueryHandler(
    ISectionRepository sectionRepository,
    IAnswerOptionRepository answerOptionRepository
) : IQueryHandler<GetTestBySectionIdWithAnswerOptionsQuery, Result<List<GetQuestionWithOptionsDto>>>
{
    public async Task<Result<List<GetQuestionWithOptionsDto>>> HandleAsync(GetTestBySectionIdWithAnswerOptionsQuery query)
    {
        var sectionExists=await sectionRepository.GetByIdAsync(query.SectionId);
        if(sectionExists==null)
                return Result<List<GetQuestionWithOptionsDto>>.Fail("Section not found",ErrorType.NotFound);
        var questions = await sectionRepository.GetRandomQuestionsAsync(query.SectionId);

        var questionAndOptionsList = new List<GetQuestionWithOptionsDto>();
        foreach (var q in questions)
        {
            var answerOptions = await answerOptionRepository.GetRandomedAnswerOptionsByQuestionIdAsync(q.Id);
            questionAndOptionsList.Add(q.ToDtoWithOptions(answerOptions));
        }

        return Result<List<GetQuestionWithOptionsDto>>.Ok(questionAndOptionsList);
    }
}