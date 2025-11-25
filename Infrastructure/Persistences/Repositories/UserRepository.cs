using System;
using System.Security.Claims;
using Domain.Entities;
using Domain.Enums;
using Domain.Filters;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class UserRepository(
    ApplicationDbContext context
) : IUserRepository
{
    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower().Trim());
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower().Trim());
    }

    public async Task<User?> GetByIdItemAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(u=>u.Id==id);
    }

    public async Task<int> CountActiveItemsAsync()
    {
        return await context.Users.CountAsync(u => u.Role == Role.User && u.IsActive);
    }

    public async Task<List<User>> GetFilteredItemsAsync(UserFilter filter)
    {
        var query= context.Users.AsQueryable();
        if(!string.IsNullOrWhiteSpace(filter.FullName))
            query=query.Where(u=>EF.Functions.ILike(u.FullName, $"%{filter.FullName}%"));
        if(!string.IsNullOrWhiteSpace(filter.Email))
            query=query.Where(u=>EF.Functions.ILike(u.Email, $"%{filter.Email}%"));
        if(filter.BirthDate.HasValue)
            query=query.Where(u=>u.BirthDate==filter.BirthDate);
        if(filter.Role.HasValue)
            query=query.Where(u=>u.Role==filter.Role);
        if (filter.IsActive.HasValue)
        {
            var isActive= filter.IsActive.Value;
            query=query.Where(u=>u.IsActive==isActive);
        }
        var result= await query
        .Skip((filter.Page-1)*filter.Size)
        .Take(filter.Size)
        .ToListAsync();
        return result;

    }

}
