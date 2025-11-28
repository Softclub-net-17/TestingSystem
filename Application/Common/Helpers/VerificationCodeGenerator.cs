using System;

namespace Application.Common.Helpers;

public static class VerificationCodeGenerator
{
    public static string GenerateCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
