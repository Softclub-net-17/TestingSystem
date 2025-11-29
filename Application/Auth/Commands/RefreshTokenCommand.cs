using Application.Auth.DTOs;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class RefreshTokenCommand(string token) : ICommand<Result<AuthResponseDto>>
{
    public string Token { get; set; } = token;
}