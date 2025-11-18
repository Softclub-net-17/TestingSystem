using System;

namespace Application.Topics.DTOs;

public class GetTopicDto
{
    public int Id{get;set;}
    public int SectionId{get;set;}
    public string Title{get;set;}=string.Empty;
    public string Content{get;set;}=string.Empty;
    public bool IsPublished{get;set;}
}
