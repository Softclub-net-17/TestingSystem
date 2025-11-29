using System;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Auth.Mappers;

public static class AuthMapper
{
    public static User ToEntity(this RegisterCommand command, string passwordHash)
    {
        return new User()
        {
            FullName = command.FullName,
            Email = command.Email,
            Role = Role.User,
            BirthDate = command.BirthDate,
            PasswordHash = passwordHash,
            IsActive=true
        };
    }

    public static void MapFrom(this User user, string passwordHash)
    {
        user.PasswordHash = passwordHash;
    }
    
    public static AuthResponseDto ToDto(string accessToken, RefreshToken refreshToken)
    {
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt
        };
    }
}
