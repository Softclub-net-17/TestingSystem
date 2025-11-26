using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Statistics.Queries;
using Domain.DTOs;
using Domain.Interfaces;

namespace Application.Statistics.Handlers;

public class GetAvarageSectionStatisticQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetAvarageSectionStatisticQuery, Result<List<AvarageSectionStatisticDto>>>
{
    public async Task<Result<List<AvarageSectionStatisticDto>>> HandleAsync(GetAvarageSectionStatisticQuery query)
    {
        var result= await sectionRepository.GetSectionStatisticsAsync();
        return Result<List<AvarageSectionStatisticDto>>.Ok(result);
    }
}
