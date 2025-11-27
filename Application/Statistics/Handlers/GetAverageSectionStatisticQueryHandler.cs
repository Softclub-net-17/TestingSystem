using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Statistics.Queries;
using Domain.DTOs;
using Domain.Interfaces;

namespace Application.Statistics.Handlers;

public class GetAverageSectionStatisticQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetAverageSectionStatisticQuery, Result<List<AvarageSectionStatisticDto>>>
{
    public async Task<Result<List<AvarageSectionStatisticDto>>> HandleAsync(GetAverageSectionStatisticQuery query)
    {
        var result= await sectionRepository.GetSectionStatisticsAsync();
        return Result<List<AvarageSectionStatisticDto>>.Ok(result);
    }
}
