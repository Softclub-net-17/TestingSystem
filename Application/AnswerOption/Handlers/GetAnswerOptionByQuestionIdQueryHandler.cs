using Application.AnswerOption.DTOs;
using Application.AnswerOption.Mappers;
using Application.AnswerOption.Queries;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.AnswerOption.Handlers;

public class GetAnswerOptionByQuestionIdQueryHandler(
    IAnswerOptionRepository answerOptionRepository,
    IQuestionRepository questionRepository)
    : IQueryHandler<GetAnswerOptionByQuestionIdQuery, Result<List<GetAnswerOptionDto>>>
{
    public async Task<Result<List<GetAnswerOptionDto>>> HandleAsync(GetAnswerOptionByQuestionIdQuery query)
    {
        var questionExists = await questionRepository.GetByIdAsync(query.QuestionId);
        if (questionExists == null)
            return Result<List<GetAnswerOptionDto>>.Fail($"Question with given id: {query.QuestionId} not found", ErrorType.NotFound);
        

        var response = await answerOptionRepository.GetByQuestionIdAsync(query.QuestionId);
        var items = response.ToDto();

        return Result<List<GetAnswerOptionDto>>.Ok(items);
    }
}