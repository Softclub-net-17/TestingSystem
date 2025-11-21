using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.DTOs;
using Application.TestSessions.Mappers;
using Application.TestSessions.Queries;
using Domain.Interfaces;

namespace Application.TestSessions.Handlers;

public class GetTestSessionsQueryHandler(ITestSessionRepository testSessionRepository) : IQueryHandler<GetTestSessionsQuery, PagedResult<List<GetTestSessionDto>>>
{
    public async Task<PagedResult<List<GetTestSessionDto>>> HandleAsync(GetTestSessionsQuery query)
    {
        var filter= query.ToFilter();
        var testSessions= await testSessionRepository.GetItemsAsync(filter);
        var items= testSessions.Items.ToDto();
        return PagedResult<List<GetTestSessionDto>>.Ok(items, filter.Page, filter.Size,testSessions.TotalCount);
    
    }
}
