using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class VerifyCodeCommand : ICommand<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
