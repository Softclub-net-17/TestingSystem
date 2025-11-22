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
            FullName = command.FullName,
            Email = command.Email,
            Role = Role.User,
            BirthDate = command.BirthDate,
            PasswordHash = passwordHash,
            IsActive=true
        };
    }
}
