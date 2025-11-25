using System;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> FindByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<List<User>> GetFilteredItemsAsync(UserFilter filter);
    Task<User?> GetByIdItemAsync(int id);
    Task<int> CountActiveItemsAsync();
}
