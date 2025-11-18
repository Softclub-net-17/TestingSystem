using System;
using System.Security.Cryptography.X509Certificates;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;
using Application.Sections.Mappers;
using Application.Sections.Queries;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class GetSectionByIdQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetSectionByIdQuery, Result<GetSectionDTO>>
{
    public async Task<Result<GetSectionDTO>> HandleAsync(GetSectionByIdQuery query)
    {
        var exist= await sectionRepository.GetByIdAsync(query.Id);
        if(exist==null)
            return Result<GetSectionDTO>.Fail($"Section with this id: {query.Id} not found",ErrorType.NotFound);
        var section=exist.ToDto();
        return Result<GetSectionDTO>.Ok(section);
    }
}
