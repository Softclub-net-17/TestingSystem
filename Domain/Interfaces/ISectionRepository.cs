using System;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface ISectionRepository
{
    public Task<(List<Section> Items, int TotalCount)>GetItemsAsync(SectionFilter filter);
    public Task<Section?> GetByIdAsync(int id);
    public Task CreateItemsAsync(Section section);
    public Task<Section?> GetItemByNameAsync(string name);
    Task<List<Question>> GetRandomQuestionsAsync(int sectionId);
    Task<int> CountActiveAsync();
    Task<decimal> GetAverageScorePercentAsync();
    Task<List<AvarageSectionStatisticDto>> GetSectionStatisticsAsync();

}
