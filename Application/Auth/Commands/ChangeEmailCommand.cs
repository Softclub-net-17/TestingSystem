using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class ChangeEmailCommand:ICommand<Result<string>>
{
    [JsonIgnore]
    public int UserId{get;set;}
    public string Email{get;set;}=string.Empty;
    public string Code { get; set; } = string.Empty;
}
