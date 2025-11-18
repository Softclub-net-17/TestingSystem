namespace Application.Questions.DTOs;

public class GetQuestionDto
{
    public int Id{get;set;}
    public int TopicId{get;set;}
    public string Text{get;set;}=null!;
    public bool IsActive{get;set;}
}