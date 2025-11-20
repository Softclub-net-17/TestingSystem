using Application.Common.Results;
using Application.Interfaces;

namespace Application.Questions.Commands;

public class ChangeQuestionStatusCommand(int id, bool isActive):ICommand<Result<string>>
{
    public int Id { get; set; } = id;
    public bool IsActive { get; set; } = isActive;
}