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

    public async Task<Section?> GetByIdAsync(int id)
    {
        return await context.Sections.Include(s=>s.Topics).FirstOrDefaultAsync(s=>s.Id==id);
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
    if (filter.TotalTopics.HasValue)
        query = query.Where(s =>context.Topics.Count(t => t.SectionId == s.Id) >= filter.TotalTopics.Value);
    if (filter.TotalQuestion.HasValue)
        query = query.Where(s =>context.Questions.Count(q => q.Topic.SectionId == s.Id) >= filter.TotalQuestion.Value);

    var totalCount = await query.CountAsync();

    var items = await query
        .OrderBy(s => s.Id) 
        .Skip((filter.Page - 1) * filter.Size)
        .Take(filter.Size)
        .ToListAsync();

    return (items, totalCount);
}
    public async Task<List<Question>> GetRandomQuestionsAsync(int sectionId)
    {
        return await context.Questions
            .Include(q=>q.Topic)
            .Where(q=> q.Topic.SectionId == sectionId
                       && q.IsActive
                       && q.Topic.IsPublished)
            .OrderBy(q => EF.Functions.Random())
            .Take(10)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> CountActiveAsync()
    {
        return await context.Sections.CountAsync(s => s.IsActive);
    }

    public async Task<decimal> GetAverageScorePercentAsync()
    {
        var sectionMaxAvarage = await context.TestSessions
            .GroupBy(ts => ts.SectionId)
            .Select(g => g.Max(ts => ts.ScorePercent))
            .ToListAsync();

        return sectionMaxAvarage.Count == 0
            ? 0m
            : sectionMaxAvarage.Average();
    }

}
