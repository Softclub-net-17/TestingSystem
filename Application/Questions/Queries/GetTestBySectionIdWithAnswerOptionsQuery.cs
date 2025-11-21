using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;

namespace Application.Questions.Queries;

public class GetTestBySectionIdWithAnswerOptionsQuery(int sectionId) 
    : IQuery<Result<List<GetQuestionWithOptionsDto>>>
{
    [JsonIgnore]
    public int SectionId { get; set; } = sectionId;
}