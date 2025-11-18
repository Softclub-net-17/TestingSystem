using System;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class SectionRepository(ApplicationDbContext context) : ISectionRepository
{
    public async Task CreateItemsAsync(Section section)
    {
        await context.Sections.AddAsync(section);
    }

    public async Task<List<Section>> GetActiveItemsAsync()
    {
        return await context.Sections.Where(s=>s.IsActive).ToListAsync();
    }

    public async Task<Section?> GetByIdAsync(int id)
    {
        return await context.Sections.FirstOrDefaultAsync(s=>s.Id==id);
    }

    public async Task<Section?> GetItemByNameAsync(string name)
    {
        return await context.Sections.FirstOrDefaultAsync(s => s.Name.Trim().ToLower() == name.Trim().ToLower());

    }

    public async Task<(List<Section> Items, int TotalCount)> GetItemsAsync(SectionFilter filter)
{
    var query = context.Sections.AsQueryable();

    if (!string.IsNullOrWhiteSpace(filter.Name))
        query = query.Where(s => EF.Functions.ILike(s.Name, $"%{filter.Name}%"));

    if (filter.IsActive.HasValue)
        query = query.Where(s => s.IsActive == filter.IsActive.Value);

    var totalCount = await query.CountAsync();

    var items = await query
        .OrderBy(s => s.Id) 
        .Skip((filter.Page - 1) * filter.Size)
        .Take(filter.Size)
        .ToListAsync();

    return (items, totalCount);
}
}
