using System;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.DTOs;

namespace Application.TestSessions.Queries;

public class GetTestSessionByIdQuery(int id):IQuery<Result<GetTestSessionDto>>
{
    [JsonIgnore]
    public int Id{get;set;}=id;
}
