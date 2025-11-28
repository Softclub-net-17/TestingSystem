using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class ResetPasswordCommand : ICommand<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
