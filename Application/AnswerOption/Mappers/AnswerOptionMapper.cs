using Application.AnswerOption.Commands;
using Application.AnswerOption.DTOs;

namespace Application.AnswerOption.Mappers;

public static class AnswerOptionMapper
{
    public static Domain.Entities.AnswerOption ToEntity(this CreateAnswerOptionCommand command)
    {
        return new Domain.Entities.AnswerOption
        {
            QuestionId = command.QuestionId,
            Text = command.Text,
            IsCorrect = command.IsCorrect
        };
    }
    
    
    public static GetAnswerOptionDto ToDto(this Domain.Entities.AnswerOption option)
    {
        return new GetAnswerOptionDto
        {
            Id = option.Id,
            QuestionId = option.QuestionId,
            Text = option.Text
        };
    }

    public static List<GetAnswerOptionDto> ToDto(this List<Domain.Entities.AnswerOption> options)
    {
        return options.Select(o => o.ToDto()).ToList();
    }

    public static GetAnswerOptionsForUserDto ToUserDto(this Domain.Entities.AnswerOption option)
    {
        return new GetAnswerOptionsForUserDto
        {
            Id = option.Id,
            QuestionId = option.QuestionId,
            Text = option.Text
        };
    }

    public static List<GetAnswerOptionsForUserDto> ToUSerDto(this List<Domain.Entities.AnswerOption> options)
    {
        return options.Select(o => o.ToUserDto()).ToList();
    }
    
    public static void MapFrom(this UpdateAnswerOptionCommand command, Domain.Entities.AnswerOption option)
    {
        option.QuestionId = command.QuestionId;
        option.Text = command.Text;
        option.IsCorrect = command.IsCorrect;
    }
}