using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Queries;
using Application.Topics.DTOs;
using Application.Topics.Mappers;
using Application.Topics.Queries;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class GetActiveTopicsQueryHandler(ITopicRepository topicRepository) : IQueryHandler<GetActiveTopicsQuery, Result<List<GetTopicDto>>>
{
    public async Task<Result<List<GetTopicDto>>> HandleAsync(GetActiveTopicsQuery query)
    {
        var response= await topicRepository.GetActiveItemsAsync();
        var items= response.ToDto();
        return Result<List<GetTopicDto>>.Ok(items);
    }
}
