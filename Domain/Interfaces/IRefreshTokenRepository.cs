using Domain.Entities;

namespace Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task CreateAsync(RefreshToken token);
    Task<RefreshToken?> FindByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> FindByUserIdAsync(int userId);
    Task UpdateAsync(RefreshToken token);
}