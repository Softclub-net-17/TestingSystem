using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;
using Application.Topics.Mappers;
using Application.Topics.Queries;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class GetTopicByIdQueryHandler(ITopicRepository topicRepository) : IQueryHandler<GetTopicByIdQuery, Result<GetTopicDto>>
{
    public async Task<Result<GetTopicDto>> HandleAsync(GetTopicByIdQuery query)
    {
        var exists=await topicRepository.GetItemByIdAsync(query.Id);
        if(exists==null)
            return Result<GetTopicDto>.Fail($"Topic with given id: {query.Id} not found",ErrorType.NotFound);
        var topic=exists.ToDto();
        return Result<GetTopicDto>.Ok(topic);
    }
}
