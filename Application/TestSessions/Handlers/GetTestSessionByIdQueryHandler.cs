using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.DTOs;
using Application.TestSessions.Mappers;
using Application.TestSessions.Queries;
using Domain.Interfaces;

namespace Application.TestSessions.Handlers;

public class GetTestSessionByIdQueryHandler(ITestSessionRepository testSessionRepository) : IQueryHandler<GetTestSessionByIdQuery, Result<GetTestSessionDto>>
{

    async Task<Result<GetTestSessionDto>> IQueryHandler<GetTestSessionByIdQuery, Result<GetTestSessionDto>>.HandleAsync(GetTestSessionByIdQuery query)
    {
        var testSessions= await testSessionRepository.GetItemByIdAsync(query.Id);
        if(testSessions==null)
            return Result<GetTestSessionDto>.Fail($"Test Session with given id: {query.Id} doesnt exists",ErrorType.NotFound);
        var item= testSessions.ToDto();
        return Result<GetTestSessionDto>.Ok(item);
    }
}
