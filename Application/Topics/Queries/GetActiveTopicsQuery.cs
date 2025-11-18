using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;

namespace Application.Topics.Queries;

public class GetActiveTopicsQuery:IQuery<Result<List<GetTopicDto>>>
{

}
