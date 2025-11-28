using System;

namespace Domain.Entities;

public class VerificationCode
{
    public int Id { get; set; }
    public string Code { get; set; }=null!;
    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; }
    public string Email { get; set; }=null!;
}
