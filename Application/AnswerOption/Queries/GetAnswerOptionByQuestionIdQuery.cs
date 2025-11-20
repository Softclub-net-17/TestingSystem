using System.Text.Json.Serialization;
using Application.AnswerOption.DTOs;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.AnswerOption.Queries;

public class GetAnswerOptionByQuestionIdQuery(int  questionId) : IQuery<Result<GetAnswerOptionDto>>, IQuery<Result<List<GetAnswerOptionDto>>>
{
    [JsonIgnore]
    public int QuestionId { get; set; } =  questionId;
}