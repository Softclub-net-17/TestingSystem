using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;

namespace Application.Questions.Queries;

public class GetQuestionsByTopicIdQuery(int topicId) : IQuery<Result<List<GetQuestionDto>>>
{
    public int TopicId { get; set; } = topicId;
}