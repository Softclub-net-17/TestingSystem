using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Users.Commands;

public class DeleteUserCommand(int id):ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id{get;set;}=id;
}
