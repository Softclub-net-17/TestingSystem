using Application.Questions.Commands;
using Application.Questions.DTOs;
using Domain.Entities;

namespace Application.Questions.Mappers;

public static class QuestionMapper
{
    public static Question ToEntity(this CreateQuestionCommand command)
    {
        return new Question()
        {
            TopicId = command.TopicId,
            Text = command.Text,
            IsActive = false
        };
    }

    public static List<GetQuestionDto> ToDto(this List<Question> questions)
    {
        return questions.Select(question => new GetQuestionDto
        { 
            Id = question.Id, 
            TopicId = question.TopicId, 
            Text = question.Text, 
            IsActive = question.IsActive
        }).ToList();
    }
    
    public static List<GetActiveQuestionsDto> ToUserDto(this List<Question> questions)
    {
        return questions.Select(question => new GetActiveQuestionsDto()
        { 
            Id = question.Id, 
            TopicId = question.TopicId, 
            Text = question.Text
        }).ToList();
    }
    
    public static GetQuestionDto ToDto(this Question question)
    {
        return new GetQuestionDto
        {
            Id = question.Id,
            TopicId = question.TopicId,
            Text = question.Text,
            IsActive = question.IsActive
        };
    }
    
    public static void ChangeStatus(this Question question, bool status)
    {
        question.IsActive = status;
    }
    
    public static void MapFrom(this UpdateQuestionCommand command, Question question)
    {
        question.TopicId = command.TopicId;
        question.Text = command.Text;
    }
}