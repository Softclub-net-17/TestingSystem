using System;

namespace Domain.Filters;

public class TopicFilter
{
    public string? Title{get;set;}
    public bool? IsPublished{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
