using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Mappers;
using Application.Auth.Utilities;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IValidator<RefreshTokenCommand> validator,
    IUnitOfWork unitOfWork)
        : ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(RefreshTokenCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors);
            return Result<AuthResponseDto>.Fail(errors, ErrorType.Validation);
        }

        var storedToken = await refreshTokenRepository.FindByTokenAsync(command.Token);

        if (storedToken is null || !storedToken.IsActive || storedToken.ExpiresAt < DateTime.UtcNow)
            return Result<AuthResponseDto>.Fail("Invalid refresh token", ErrorType.Unauthorized);

        var user = await userRepository.GetByIdItemAsync(storedToken.UserId);
        if (user is null)
            return Result<AuthResponseDto>.Fail("User not found", ErrorType.NotFound);

        storedToken.IsActive = false;
        await refreshTokenRepository.UpdateAsync(storedToken);

        var newRefreshToken = RefreshTokenGenerator.Generate(user.Id);
        await refreshTokenRepository.CreateAsync(newRefreshToken);

        var newAccessToken = jwtTokenGenerator.GenerateToken(user);

        await unitOfWork.SaveChangesAsync();

        var response = AuthMapper.ToDto(newAccessToken, newRefreshToken);
        return Result<AuthResponseDto>.Ok(response, "Token refreshed successfully.");
    }
}