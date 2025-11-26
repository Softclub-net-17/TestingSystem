using System;

namespace Domain.Filters;

public class SectionFilter
{
    public string? Name {get;set;}
    public bool? IsActive{get;set;}
    public int? TotalTopics{get;set;}
    public int? TotalQuestion{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
