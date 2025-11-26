using System;

namespace Application.Sections.DTOs;

public class GetSectionByIdDto
{
    public int Id {get;set;}
    public string Name {get;set;}=string.Empty;
    public bool IsActive{get;set;}
    public int TopicCount{get;set;}
}
