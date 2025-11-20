using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;

public class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
{
    public async Task<Question?> GetByIdAsync(int id)
    {
        return await context.Questions.Include(q=>q.AnswerOptions).FirstOrDefaultAsync(s=>s.Id==id);
    }

    public async Task<List<Question>> GetByTopicIdAsync(int topicId)
    {
        return await context.Questions.Include(q=>q.AnswerOptions).Where(q=>q.TopicId==topicId).ToListAsync();
    }

    public async Task<List<Question>> GetActiveItemsAsync()
    {
        return await context.Questions
        .Include(q => q.AnswerOptions)
        .Where(q => q.IsActive)
        .ToListAsync();
    }

    
    public async Task<List<Question>> GetAllAsync()
    {
        return await context.Questions.ToListAsync();
    }

    public async Task Create(Question question)
    {
        await context.Questions.AddAsync(question);
    }

    
    // Returns 10 random questions for a given section id
    public async Task<List<Question>> GetRandomedTestsBySectionIdAsync(int sectionId)
    {
        return await context.Questions
            .Where(q=> q.Topic.SectionId == sectionId
                                && q.IsActive
                                && q.Topic.IsPublished)
            .OrderBy(q => EF.Functions.Random())
            .Take(10)
            .AsNoTracking()
            .ToListAsync();
    }
}