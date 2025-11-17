using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Enums;

namespace Application.Users.Commands;

public class UpdateUserCommand:ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Role Role { get; set; }
    public bool IsActive{get;set;}


}
