using System;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface ITopicRepository
{
    public Task<List<Topic>> GetItemsAsync(TopicFilter filter);
    public Task<List<Topic>> GetActiveItemsAsync();
    public Task<Topic?> GetByIdItemAsync(int id);
    public Task CreateItemAsync(Topic topic);
    public Task<Topic?> GetItemByNameAsync(string name);
    public Task<List<Topic>> GetTopicBySectionIdAsync(int sectionId);

}
