using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.AnswerOption.Commands;

public class DeleteAnswerOptionCommand(int id) : ICommand<Result<string>>
{
    [JsonIgnore] public int Id { get; set; } = id;
}