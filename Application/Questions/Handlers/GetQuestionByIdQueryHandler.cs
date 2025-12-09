using Application.Common.Results;
using Application.Interfaces;
using Application.Questions.DTOs;
using Application.Questions.Mappers;
using Application.Questions.Queries;
using Domain.Interfaces;

namespace Application.Questions.Handlers;

public class GetQuestionByIdQueryHandler(IQuestionRepository questionRepository)
    : IQueryHandler<GetQuestionByIdQuery, Result<GetQuestionDto>>
{
    public async Task<Result<GetQuestionDto>> HandleAsync(GetQuestionByIdQuery query)
    {
        var exists=await questionRepository.GetByIdAsync(query.Id);
        
        if(exists==null)
            return Result<GetQuestionDto>
                .Fail($"Question with given id: {query.Id} not found",ErrorType.NotFound);
        
        var question = exists.ToDto();
        return Result<GetQuestionDto>.Ok(question);
    }
}