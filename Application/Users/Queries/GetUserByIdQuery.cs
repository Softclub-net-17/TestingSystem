using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.DTOs;

namespace Application.Users.Queries;

public class GetUserByIdQuery(int id):IQuery<Result<GetUserDTO>>
{
    public int Id{get;set;}=id;
}
