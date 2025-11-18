using System;
using Application.Sections.Commands;
using Application.Sections.DTOs;
using Application.Sections.Queries;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Filters;

namespace Application.Sections.Mappers;

public static class SectionMapper
{
    public static Section ToEntity(this CreateSectionCommand command)
    {
        return new Section
        {
            Name=command.Name.Trim(),
            IsActive=true
        };
    }

    public static List<GetSectionDTO> ToDto(this List<Section> sections)
    {
        return sections.Select(s=>new GetSectionDTO
        {
            Id=s.Id,
            Name=s.Name,
            IsActive=s.IsActive
        }).ToList();
    }
    public static GetSectionDTO ToDto(this Section section)
    {
        return new GetSectionDTO
        {
            Id=section.Id,
            Name=section.Name,
            IsActive=section.IsActive
        };
    }

    public static void MapFrom(this UpdateSectionCommand command, Section section)
    {
        section.Name=command.Name;
    }
    public static void ChangeStatus(this Section section, bool status)
    {
        section.IsActive=status;
    }
    public static SectionFilter ToFilter(this GetSectionsQuery query)
    {
        return new SectionFilter
        {
            Name=query.Name,
            IsActive=query.IsActive,
            Page=query.Page,
            Size=query.Size
        };
    }
}
