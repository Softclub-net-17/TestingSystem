using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class RegisterCommand:ICommand<Result<string>>
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
