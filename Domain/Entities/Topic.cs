using System;
using System.Diagnostics.Contracts;

namespace Domain.Entities;

public class Topic
{
    public int Id{get;set;}
    public int SectionId{get;set;}
    public string Title{get;set;}=null!;
    public string Content{get;set;}=string.Empty;
    public bool IsPublished{get;set;}

    public Section Section{get;set;}=null!;
    public List<Question> Questions{get;set;}=[];
    public List<TestSession> TestSessions{get;set;}=[];
    

}
