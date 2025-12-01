using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
{
    public async Task CreateAsync(RefreshToken token)
    {
        await context.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken?> FindByTokenAsync(string token)
    {
        return await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> FindByUserIdAsync(int userId)
    {
        return await context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .ToListAsync();
    }

    public Task UpdateAsync(RefreshToken token)
    {
        context.RefreshTokens.Update(token);
        return Task.CompletedTask;
    } 
}