using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;
using Application.Sections.Mappers;
using Application.Sections.Queries;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class GetSectionsQueryHandler(ISectionRepository sectionRepository) : IQueryHandler<GetSectionsQuery, PagedResult<List<GetSectionDTO>>>
{
    public async Task<PagedResult<List<GetSectionDTO>>> HandleAsync(GetSectionsQuery query)
    {
        var filter= query.ToFilter();
        var response= await sectionRepository.GetItemsAsync(filter);
        var items= response.Items.ToDto();
        return PagedResult<List<GetSectionDTO>>.Ok(items,filter.Page, filter.Size,response.TotalCount);
    }
}
