using Domain.Entities;

namespace Domain.Interfaces;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int id);
    Task<List<Question>> GetByTopicIdAsync(int topicId);
    Task<List<Question>> GetActiveItemsAsync();
    Task<List<Question>> GetAllAsync();
    Task Create(Question question);
}