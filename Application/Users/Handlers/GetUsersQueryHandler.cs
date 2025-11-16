using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.DTOs;
using Application.Users.Mappers;
using Application.Users.Queries;
using Domain.Interfaces;

namespace Application.Users.Handlers;

public class GetUsersQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUsersQuery, PagedResult<List<GetUserDTO>>>
{
    public async Task<PagedResult<List<GetUserDTO>>> HandleAsync(GetUsersQuery query)
    {
        var filter= query.ToFilter();
        var request= await userRepository.GetFilteredItemsAsync(filter);
        var totalCount = request.Count;
        var result= request.ToDto();
        return PagedResult<List<GetUserDTO>>.Ok(result, filter.Page, filter.Size,totalCount);

    }
}
