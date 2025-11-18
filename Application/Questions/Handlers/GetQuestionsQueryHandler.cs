using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetQuestionsQueryHandler(IQuestionRepository questionRepository)
    : IQueryHandler<GetQuestionsQuery, Result<List<GetQuestionDto>>>
{
    public async Task<Result<List<GetQuestionDto>>> HandleAsync(GetQuestionsQuery query)
    {
        var questions = await questionRepository.GetAllAsync();

        var items = questions.ToDto();

        return Result<List<GetQuestionDto>>.Ok(items);
    }
}