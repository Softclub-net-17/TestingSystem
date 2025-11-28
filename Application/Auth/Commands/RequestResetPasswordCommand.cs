using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class RequestResetPasswordCommand : ICommand<Result<string>>
{
    public string Email { get; set; } = string.Empty;
}
