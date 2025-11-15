using System;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
    public string PasswordHash { get; set; } = null!;

    public List<TestSession> TestSessions{get;set;}=[];
}
