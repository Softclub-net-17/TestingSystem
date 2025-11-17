using System;
using Application.Auth.Commands;
using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Queries;
using Domain.Entities;
using Domain.Filters;

namespace Application.Users.Mappers;


public static class UserMapper
{
    public static GetUserDTO ToDto(this User user)
    {
        return new GetUserDTO
        {
            Id=user.Id,
            FullName=user.FullName,
            Email=user.Email,
            BirthDate=user.BirthDate,
            Role=user.Role
        };
    }
   
    public static List<GetUserDTO> ToDto(this List<User> users)
    {
        return users.Select(u => new GetUserDTO
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            Role = u.Role,
            BirthDate = u.BirthDate
        }).ToList();
    }

    public static void MapFrom(this UpdateUserCommand command, User user)
    {
        user.FullName=command.FullName;
        user.BirthDate=command.BirthDate;
        user.Role=command.Role;
        user.IsActive=command.IsActive;
    }

    public static void InActive(this User user)
    {
        user.IsActive=false;
    }

    public static UserFilter ToFilter(this GetUsersQuery query)
    {
        return new UserFilter
        {
            FullName=query.FullName,
            Email=query.Email,
            BirthDate=query.BirthDate,
            IsActive=query.IsActive,
            Role=query.Role,
            Page=query.Page,
            Size=query.Size
        };
    }
}

