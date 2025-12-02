using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class RequestChangeEmailCommand:ICommand<Result<string>>
{
    [JsonIgnore]
    public int UserId{get;set;}
    public string NewEmail { get; set; } = null!;
}
