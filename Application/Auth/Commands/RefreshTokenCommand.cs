using Application.Auth.DTOs;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class RefreshTokenCommand : ICommand<Result<AuthResponseDto>>
{
    public string Token { get; set; } = null!;
}