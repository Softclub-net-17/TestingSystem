using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Application.Topics.DTOs;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetActiveQuestionsQueryHandler(IQuestionRepository questionRepository)
    : IQueryHandler<GetActiveQuestionsQuery, Result<List<GetActiveQuestionsDto>>>, IQuery<Result<List<GetQuestionDto>>>
{
    public async Task<Result<List<GetActiveQuestionsDto>>> HandleAsync(GetActiveQuestionsQuery query)
    {
        var response= await questionRepository.GetActiveItemsAsync();
        var items= response.ToUserDto();
        return Result<List<GetActiveQuestionsDto>>.Ok(items);
    }
}