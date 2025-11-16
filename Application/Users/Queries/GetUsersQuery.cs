using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.DTOs;
using Domain.Enums;

namespace Application.Users.Queries;

public class GetUsersQuery:IQuery<PagedResult<List<GetUserDTO>>>
{
    public string? FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }
    public bool? IsActive{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
