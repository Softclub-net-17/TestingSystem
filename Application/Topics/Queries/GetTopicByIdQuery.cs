using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.DTOs;

namespace Application.Topics.Queries;

public class GetTopicByIdQuery(int id):IQuery<Result<GetTopicDto>>
{
    public int Id{get;set;}=id;
}
