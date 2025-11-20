using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;

namespace Application.Questions.Queries;

public class GetQuestionsQuery : ICommand<Result<List<GetQuestionDto>>>, IQuery<Result<List<GetQuestionDto>>>
{
    
}