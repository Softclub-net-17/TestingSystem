using Application.AnswerOption.DTOs;
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
        return questions.Select(q => new GetActiveQuestionsDto
        {
            Id = q.Id,
            TopicId = q.TopicId,
            Text = q.Text,
            Answers = q.AnswerOptions.Select(a => new GetAnswerOptionsForUserDto
            {
                Id = a.Id,
                QuestionId = a.QuestionId,
                Text = a.Text
            }).ToList()
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
    
    public static GetQuestionWithOptionsDto ToWithOptionsDto(this Question question, List<Domain.Entities.AnswerOption> answerOptions)
    {
        return new GetQuestionWithOptionsDto
        {
            Id = question.Id,
            TopicId = question.TopicId,
            Text = question.Text,
            IsActive = question.IsActive,
            AnswerOptions = answerOptions.Select(ao => new GetAnswerOptionDto
            {
                Id = ao.Id,
                Text = ao.Text,
                IsCorrect = ao.IsCorrect
            }).ToList()
        };
    }
}