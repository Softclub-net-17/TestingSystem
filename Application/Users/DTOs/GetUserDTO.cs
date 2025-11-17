using System;
using Domain.Enums;

namespace Application.Users.DTOs;
public class GetUserDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
}
