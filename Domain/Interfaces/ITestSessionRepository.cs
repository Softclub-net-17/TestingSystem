using System;
using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces;

public interface ITestSessionRepository
{
    Task<(List<TestSession> Items, int TotalCount)> GetItemsAsync(TestSessionFilter filter);
    Task<TestSession?> GetItemByIdAsync(int id);
    Task CreateItemAsync(TestSession testSession);
    
}
