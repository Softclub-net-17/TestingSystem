using System;
using System.Security.Cryptography.X509Certificates;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;
using Application.Sections.Mappers;
using Application.Sections.Queries;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class GetSectionByIdQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetSectionByIdQuery, Result<GetSectionByIdDto>>
{
    public async Task<Result<GetSectionByIdDto>> HandleAsync(GetSectionByIdQuery query)
    {
        var exist= await sectionRepository.GetByIdAsync(query.Id);
        if(exist==null)
            return Result<GetSectionByIdDto>.Fail($"Section with this id: {query.Id} not found",ErrorType.NotFound);
        var topicCount=exist.Topics.Count;
        var section=exist.ToDto(topicCount);
        return Result<GetSectionByIdDto>.Ok(section);
    }
}
