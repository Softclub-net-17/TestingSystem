using System;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface ITopicRepository
{
    public Task<(List<Topic> Items, int TotalCount)> GetItemsAsync(TopicFilter filter);
    public Task<Topic?> GetItemByIdAsync(int id);
    public Task CreateItemAsync(Topic topic);
    public Task<Topic?> GetItemByNameAsync(string name);
    public Task<List<Topic>> GetTopicBySectionIdAsync(int sectionId);

}
