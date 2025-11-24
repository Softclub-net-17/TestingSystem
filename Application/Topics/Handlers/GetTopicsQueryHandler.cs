using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;
using Application.Topics.Mappers;
using Application.Topics.Queries;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class GetTopicsQueryHandler(ITopicRepository topicRepository) : IQueryHandler<GetTopicsQuery, PagedResult<List<GetTopicDto>>>
{
    public async Task<PagedResult<List<GetTopicDto>>> HandleAsync(GetTopicsQuery query)
    {
        var filter= query.ToFilter();
        var response= await topicRepository.GetItemsAsync(filter);
        var items= response.Items.ToDto();
        return PagedResult<List<GetTopicDto>>.Ok(items,filter.Page, filter.Size, response.TotalCount);
    }
}
