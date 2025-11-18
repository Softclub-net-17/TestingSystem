using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.DTOs;

namespace Application.Sections.Queries;

public class GetActiveSectionsQuery:IQuery<Result<List<GetSectionDTO>>>
{

}
