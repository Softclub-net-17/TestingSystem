using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;

namespace Application.Topics.Queries;

public class GetTopicsBySectionIdQuery(int id):IQuery<Result<List<GetTopicBySectionIdDto>>>
{
    public int SectionId{get;set;}=id;
}
