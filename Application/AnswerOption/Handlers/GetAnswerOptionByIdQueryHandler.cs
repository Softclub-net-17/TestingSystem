using Application.AnswerOption.DTOs;
using Application.AnswerOption.Mappers;
using Application.AnswerOption.Queries;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.AnswerOption.Handlers;

public class GetAnswerOptionByIdQueryHandler(
    IAnswerOptionRepository answerOptionRepository)
    : IQueryHandler<GetAnswerOptionByIdQuery, Result<GetAnswerOptionDto>>
{
    public async Task<Result<GetAnswerOptionDto>> HandleAsync(GetAnswerOptionByIdQuery query)
    {
        var exists = await answerOptionRepository.GetItemByIdAsync(query.Id);

        if (exists == null)
            return Result<GetAnswerOptionDto>
                .Fail($"Answer option with given id: {query.Id} not found", ErrorType.NotFound);

        var dto = exists.ToDto();
        return Result<GetAnswerOptionDto>.Ok(dto);
    }
}