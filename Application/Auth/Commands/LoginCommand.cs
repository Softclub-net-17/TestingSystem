using System;
using Application.Auth.DTOs;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class LoginCommand:ICommand<Result<string>>, ICommand<Result<AuthResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
