using System;

namespace Domain.Entities;

public class Question
{
    public int Id{get;set;}
    public int TopicId{get;set;}
    public int SectionId{get;set;}
    public string Text{get;set;}=null!;
    public bool IsActive{get;set;}

    public Topic Topic{get;set;}=null!;
    public Section Section{get;set;}=null!;
    public List<AnswerOption> AnswerOptions{get;set;}=[];
    
}
