using Application.AnswerOption.DTOs;

namespace Application.Questions.DTOs;

public class GetQuestionWithOptionsDto
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Text { get; set; } = null!;
    public bool IsActive { get; set; }

    public List<GetAnswerOptionDto> AnswerOptions { get; set; } = new();
}