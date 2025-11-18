using System;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface ISectionRepository
{
    public Task<(List<Section> Items, int TotalCount)>GetItemsAsync(SectionFilter filter);
    public Task<List<Section>> GetActiveItemsAsync();
    public Task<Section?> GetByIdAsync(int id);
    public Task CreateItemsAsync(Section section);
    public Task<Section?> GetItemByNameAsync(string name);

}
