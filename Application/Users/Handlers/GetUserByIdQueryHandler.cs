using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.DTOs;
using Application.Users.Mappers;
using Application.Users.Queries;
using Domain.Interfaces;

namespace Application.Users.Handlers;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, Result<GetUserDTO>>
{
    public async Task<Result<GetUserDTO>> HandleAsync(GetUserByIdQuery query)
    {
        var userDb= await userRepository.GetByIdItemAsync(query.Id);
        if(userDb==null)
            return Result<GetUserDTO>.Fail($"User by this id {query.Id} not found",ErrorType.NotFound);
        var user=userDb.ToDto();
        return Result<GetUserDTO>.Ok(user);
    }
}
