using System;
using Domain.Entities;

namespace Application.VarificationCodes.Mappers;

public static class VerificationCodeMappers
{
    public static VerificationCode ToEntity(string code, string email)
    {
        return new VerificationCode
        {
            Email = email,
            Code = code,
            Expiration = DateTime.UtcNow.AddMinutes(2),
            IsUsed = false
        };
    }
    
    public static void DeActivate(this VerificationCode code)
    {
        code.IsUsed = true;
    }
}