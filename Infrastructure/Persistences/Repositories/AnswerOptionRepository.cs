using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class AnswerOptionRepository(ApplicationDbContext context) : IAnswerOptionRepository
{
    public async Task<List<AnswerOption>> GetByQuestionIdAsync(int questionId)
    {
        return await context.AnswerOptions.Where(a => a.QuestionId == questionId).ToListAsync();
    }

    public async Task CreateAsync(AnswerOption option)
    {
        await context.AnswerOptions.AddAsync(option);
    }

    public async Task<AnswerOption?> GetItemByIdAsync(int id)
    {
        return await context.AnswerOptions.FirstOrDefaultAsync(q=>q.Id==id);
    }

    public Task DeleteAsync(AnswerOption option)
    {
        context.AnswerOptions.Remove(option);
        return Task.CompletedTask;
    }

    public async Task<int> CorrectAnswerCountAsync(List<int> ints)
    {
       var list=await context.AnswerOptions.Where(ao=>ints.Contains(ao.Id) && ao.IsCorrect).ToListAsync();
        return list.Count();
    }
}