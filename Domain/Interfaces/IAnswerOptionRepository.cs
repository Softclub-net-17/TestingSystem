using Domain.Entities;

namespace Domain.Interfaces;

public interface IAnswerOptionRepository
{
    Task<List<AnswerOption>> GetByQuestionIdAsync(int questionId);
    Task CreateAsync(AnswerOption option);
    Task<AnswerOption?> GetItemByIdAsync(int id);
    Task DeleteAsync(AnswerOption option);
    Task<int> CorrectAnswerCountAsync(List<int> ints);

}