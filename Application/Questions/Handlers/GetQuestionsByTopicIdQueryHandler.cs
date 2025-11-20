using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetQuestionsByTopicIdQueryHandler(
    IQuestionRepository  questionRepository,
    ITopicRepository  topicRepository) : IQueryHandler<GetQuestionsByTopicIdQuery, Result<List<GetQuestionDto>>>
{
    public async Task<Result<List<GetQuestionDto>>> HandleAsync(GetQuestionsByTopicIdQuery query)
    {
        var topicExists= await topicRepository.GetItemByIdAsync(query.TopicId);
        if(topicExists==null)
            return Result<List<GetQuestionDto>>
                .Fail($"Topic with given id: {query.TopicId} not found",ErrorType.NotFound);
        
        var questions= await questionRepository.GetByTopicIdAsync(query.TopicId);
        var items= questions.ToDto();
        return Result<List<GetQuestionDto>>.Ok(items);
    }
}