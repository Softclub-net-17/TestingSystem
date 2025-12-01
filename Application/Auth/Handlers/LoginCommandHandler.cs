using System;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Utilities;
using Application.Common.Results;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class LoginCommandHandler(
IUserRepository userRepository,
IValidator<LoginCommand> validator,
IJwtTokenGenerator jwtTokenGenerator,
IRefreshTokenRepository refreshTokenRepository,
IUnitOfWork unitOfWork
) : ICommandHandler<LoginCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(LoginCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<AuthResponseDto>.Fail(errors, ErrorType.Validation);
        }

        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
        {
            return Result<AuthResponseDto>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }

        var verify = PasswordHasher.Verify(command.Password, user.PasswordHash);
        if (!verify)
        {
            return Result<AuthResponseDto>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }

        // Генерация access‑token
        var accessToken = jwtTokenGenerator.GenerateToken(user);

        // Генерация refresh‑token
        var refreshToken = RefreshTokenGenerator.Generate(user.Id);
        await refreshTokenRepository.CreateAsync(refreshToken);

        await unitOfWork.SaveChangesAsync();

        // Возвращаем DTO
        var response = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };

        return Result<AuthResponseDto>.Ok(response, "Login successful.");
    }
}
