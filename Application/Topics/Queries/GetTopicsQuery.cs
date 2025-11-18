using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;

namespace Application.Topics.Queries;

public class GetTopicsQuery:IQuery<PagedResult<List<GetTopicDto>>>
{
    public string? Title{get;set;}
    public bool? IsPublished{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
