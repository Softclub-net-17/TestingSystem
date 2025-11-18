using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;

namespace Application.Sections.Queries;

public class GetSectionByIdQuery(int id):IQuery<Result<GetSectionDTO>>
{
    public int Id{get;set;}=id;
}
