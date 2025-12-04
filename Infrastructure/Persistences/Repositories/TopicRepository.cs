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


    public async Task<Topic?> GetItemByIdAsync(int id)
    {
        return await context.Topics.FirstOrDefaultAsync(s=>s.Id==id);
        
    }

    public async Task<Topic?> GetItemByNameAsync(string name, int sectionId)
    {
         return await context.Topics.FirstOrDefaultAsync(s =>EF.Functions.ILike(s.Title, name.Trim()) && s.SectionId == sectionId);
    }

    public async Task<(List<Topic> Items, int TotalCount)> GetItemsAsync(TopicFilter filter)
    {
        var query= context.Topics.AsQueryable();
        if(!string.IsNullOrWhiteSpace(filter.Title))
            query=query.Where(s=>EF.Functions.ILike(s.Title, $"%{filter.Title}%"));

        if (filter.IsPublished.HasValue)
            query=query.Where(s=>s.IsPublished==filter.IsPublished.Value);
        var totalCount= await query.CountAsync();
        var items= await query
        .Skip((filter.Page-1)*filter.Size)
        .Take(filter.Size)
        .ToListAsync();
        return (items,totalCount);
    }

    public async Task<List<Topic>> GetTopicBySectionIdAsync(int sectionId)
    {
        return await context.Topics.Include(t=>t.Questions).Where(s=>s.SectionId==sectionId).ToListAsync();
    }
}
