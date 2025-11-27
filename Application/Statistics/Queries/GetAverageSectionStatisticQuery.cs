using System;
using Application.Common.Results;
using Application.Interfaces;
using Domain.DTOs;

namespace Application.Statistics.Queries;

public class GetAverageSectionStatisticQuery:IQuery<Result<List<AvarageSectionStatisticDto>>>
{

}
