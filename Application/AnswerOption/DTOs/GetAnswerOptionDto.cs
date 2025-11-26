namespace Application.AnswerOption.DTOs;

public class GetAnswerOptionDto
{
    public int Id{get;set;}
    public int QuestionId{get;set;}
    public string Text{get;set;}=null!;
}