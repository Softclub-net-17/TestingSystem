using System;

namespace Application.AnswerOption.DTOs;

public class GetAnswerOptionsForUserDto
{
    public int Id{get;set;}
    public int QuestionId{get;set;}
    public string Text{get;set;}=null!;
}
