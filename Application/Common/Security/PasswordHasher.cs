using System;
using System.Security.Cryptography;

namespace Application.Common.Security;


public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256).GetBytes(32);
        var passwordHash = Convert.ToBase64String(salt.Concat(hash).ToArray());
        
        return passwordHash;
    }
    
    public static bool Verify(string password, string passwordHash)
    {
        var bytes = Convert.FromBase64String(passwordHash);
        var salt = bytes[..16];
        var storedHash = bytes[16..];
        var hash = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256).GetBytes(32);
        
        return hash.SequenceEqual(storedHash);
    }
}