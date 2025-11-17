using System;
using Domain.Enums;

namespace Domain.Filters;

public class UserFilter
{
    public string? FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }
    public bool? IsActive{get;set;}
    public int Page{get;set;}=1;
    public int Size{get;set;}=10;
}
