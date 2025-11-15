using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> FindByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}
