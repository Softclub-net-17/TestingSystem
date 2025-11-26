using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;

namespace Application.Sections.Queries;

public class GetSectionsQuery:IQuery<PagedResult<List<GetSectionDTO>>>
{
    public string? Name {get;set;}
    public bool? IsActive{get;set;}
    public int? TotalTopics{get;set;}
    public int? TotalQuestion{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
