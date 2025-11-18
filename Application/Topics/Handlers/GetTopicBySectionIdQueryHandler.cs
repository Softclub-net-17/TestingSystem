using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;
using Application.Topics.Mappers;
using Application.Topics.Queries;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class GetTopicBySectionIdQueryHandler(ISectionRepository sectionRepository,
ITopicRepository topicRepository) : IQueryHandler<GetTopicsBySectionIdQuery, Result<List<GetTopicDto>>>
{
    public async Task<Result<List<GetTopicDto>>> HandleAsync(GetTopicsBySectionIdQuery query)
    {
        var sectionExist= await sectionRepository.GetByIdAsync(query.SectionId);
        if(sectionExist==null)
            return Result<List<GetTopicDto>>.Fail($"Section with given id: {query.SectionId} not found",ErrorType.NotFound);
        var response= await topicRepository.GetTopicBySectionIdAsync(query.SectionId);
        var items= response.ToDto();
        return Result<List<GetTopicDto>>.Ok(items);
    }
}
