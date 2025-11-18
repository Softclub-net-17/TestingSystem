using System;
using System.Runtime.InteropServices;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;
using Application.Sections.Mappers;
using Application.Sections.Queries;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class GetActiveSectionsQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetActiveSectionsQuery, Result<List<GetSectionDTO>>>
{
    public async Task<Result<List<GetSectionDTO>>> HandleAsync(GetActiveSectionsQuery query)
    {
        var response= await sectionRepository.GetActiveItemsAsync();
        var items= response.ToDto();
        return Result<List<GetSectionDTO>>.Ok(items);
    }
}
