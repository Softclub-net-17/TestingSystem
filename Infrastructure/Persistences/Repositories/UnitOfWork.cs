using System;
using Domain.Interfaces;

namespace Infrastructure.Persistences.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}