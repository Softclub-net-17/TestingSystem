using Application.AnswerOption.DTOs;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.AnswerOption.Queries;

public class GetAnswerOptionByIdQuery(int id) : IQuery<Result<GetAnswerOptionDto>>
{
    public int Id { get; set; } = id;
}