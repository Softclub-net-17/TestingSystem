namespace Application.Questions.DTOs;

public class GetActiveQuestionsDto
{
    public int Id{get;set;}
    public int TopicId{get;set;}
    public string Text{get;set;}=null!;
}