using System;
using Application.Topics.Commands;
using Application.Topics.DTOs;
using Application.Topics.Queries;
using Domain.Entities;
using Domain.Filters;

namespace Application.Topics.Mappers;

public static class TopicMapper
{
    public static Topic ToEntity(this CreateTopicCommand command)
    {
        return new Topic
        {
            Title=command.Title.Trim(),
            SectionId=command.SectionId,
            Content=command.Content,
            IsPublished=true
        };
    }

    public static List<GetTopicDto> ToDto(this List<Topic> topics)
    {
        return topics.Select(t=>new GetTopicDto
        {
            Id=t.Id,
            Title=t.Title,
            SectionId=t.SectionId,
            Content=t.Content,
            IsPublished=t.IsPublished
        }).ToList();
    }
    public static GetTopicDto ToDto(this Topic topic)
    {
        return new GetTopicDto
        {
            Id=topic.Id,
            Title=topic.Title,
            SectionId=topic.SectionId,
            Content=topic.Content,
            IsPublished=topic.IsPublished
        };
    }

    public static void MapFrom(this UpdateTopicCommand command, Topic topic)
    {
        topic.SectionId=command.SectionId;
        topic.Title=command.Title;
        topic.Content=command.Content;
    }
    public static void ChangeStatus(this Topic topic, bool status)
    {
        topic.IsPublished=status;
    }
    public static TopicFilter ToFilter(this GetTopicsQuery query)
    {
        return new TopicFilter
        {
            Title=query.Title,
            IsPublished=query.IsPublished,
            Page=query.Page,
            Size=query.Size
        };
    }
}
