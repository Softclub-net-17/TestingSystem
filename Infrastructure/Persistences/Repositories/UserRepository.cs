using System;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower().Trim());
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower().Trim());
    }

    public async Task<List<User>> GetItemsAsync()
    {
        return await context.Users.ToListAsync();
    }
}
