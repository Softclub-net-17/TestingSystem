using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;

namespace Application.Questions.Queries;

public class GetQuestionByIdQuery(int id) : IQuery<Result<GetQuestionDto>>
{
    public int Id { get; set; } = id;
}