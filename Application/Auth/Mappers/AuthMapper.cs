using System;
using Application.Auth.Commands;
using Domain.Entities;
using Domain.Enums;

namespace Application.Auth.Mappers;

public static class AuthMapper
{
    public static User ToEntity(this RegisterCommand command, string passwordHash)
    {
        return new User()
        {
            Email = command.Email,
            Role = Role.User,
            PasswordHash = passwordHash,
            IsActive=true
        };
    }
}
