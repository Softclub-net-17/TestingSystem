using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Application.Topics.DTOs;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetActiveQuestionsQueryHandler(IQuestionRepository questionRepository)
    : IQueryHandler<GetActiveQuestionsQuery, Result<List<GetActiveQuestionsDto>>>
{
    public async Task<Result<List<GetActiveQuestionsDto>>> HandleAsync(GetActiveQuestionsQuery query)
    {
        var questions= await questionRepository.GetActiveItemsAsync();
        var items= questions.ToUserDto();
        return Result<List<GetActiveQuestionsDto>>.Ok(items);
    }
}