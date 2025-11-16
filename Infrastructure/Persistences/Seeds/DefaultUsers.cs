using System;
using Application.Common.Security;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Seeds;

public class DefaultUsers
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var admin = await context.Users.FirstOrDefaultAsync(user => user.Email == "adminadmin@gmail.com");
        if (admin != null) return;
        var passwordHash = PasswordHasher.HashPassword("admin12345");

        var newUser = new User
        {
            Email = "adminadmin@gmail.com",
            Role = Role.Admin,
            PasswordHash = passwordHash
        };
        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
    }
}
