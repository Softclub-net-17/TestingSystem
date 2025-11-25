using System;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class TestSessionRepository(ApplicationDbContext context) : ITestSessionRepository
{
    public async Task CreateItemAsync(TestSession testSession)
    {
        await context.TestSessions.AddAsync(testSession);
    }

    public async Task<decimal> GetAverageScorePercentAsync()
    {
        var userMaxScores = await context.TestSessions
            .GroupBy(ts => ts.UserId)
            .Select(g => g.Max(ts => ts.ScorePercent))
            .ToListAsync();

        return userMaxScores.Count == 0
            ? 0m
            : userMaxScores.Average();
    }

    public Task<TestSession?> GetItemByIdAsync(int id)
    {
        return context.TestSessions.FirstOrDefaultAsync(ts=>ts.Id==id);
    }

    public async Task<(List<TestSession> Items, int TotalCount)> GetItemsAsync(TestSessionFilter filter)
    {
        var query= context.TestSessions.AsQueryable();

        if(filter.TopicId.HasValue)
            query=query.Where(ts=>ts.TopicId==filter.TopicId);
        if(filter.SectionId.HasValue)
            query=query.Where(ts=>ts.SectionId==filter.SectionId);
        if(filter.UserId.HasValue)
            query=query.Where(ts=>ts.UserId==filter.UserId);
        if(filter.DueDate.HasValue)
            query = query.Where(ts => DateOnly.FromDateTime(ts.StartedAt) == filter.DueDate.Value);
        if(filter.ScorePercent.HasValue)
            query=query.Where(ts=>ts.ScorePercent>=filter.ScorePercent);
        if(filter.IsPassed.HasValue)
            query=query.Where(ts=>ts.IsPassed==filter.IsPassed.Value);
        var totalCount= await query.CountAsync();

        var items = await query
        .OrderBy(s => s.Id) 
        .Skip((filter.Page - 1) * filter.Size)
        .Take(filter.Size)
        .ToListAsync();

       return (items, totalCount);
    }
   
}
