using System;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class TopicRepository(ApplicationDbContext context) : ITopicRepository
{
    public async Task CreateItemAsync(Topic topic)
    {
        await context.Topics.AddAsync(topic);
    }

    public async Task<List<Topic>> GetActiveItemsAsync()
    {
        return await context.Topics.Where(t=>t.IsPublished).ToListAsync();
    }

    public async Task<Topic?> GetItemByIdAsync(int id)
    {
        return await context.Topics.FirstOrDefaultAsync(s=>s.Id==id);
        
    }

    public async Task<Topic?> GetItemByNameAsync(string name)
    {
        return await context.Topics.FirstOrDefaultAsync(s=>s.Title.ToLower()==name.Trim().ToLower());
        
    }

    public async Task<List<Topic>> GetItemsAsync(TopicFilter filter)
    {
        var query= context.Topics.AsQueryable();
        if(!string.IsNullOrWhiteSpace(filter.Title))
            query=query.Where(s=>EF.Functions.ILike(s.Title, $"%{filter.Title}%"));

        if (filter.IsPublished.HasValue)
            query=query.Where(s=>s.IsPublished==filter.IsPublished.Value);
        var items= await query
        .Skip((filter.Page-1)*filter.Size)
        .Take(filter.Size)
        .ToListAsync();
        return items;
    }

    public async Task<List<Topic>> GetTopicBySectionIdAsync(int sectionId)
    {
        return await context.Topics.Where(s=>s.SectionId==sectionId).ToListAsync();
    }
}
