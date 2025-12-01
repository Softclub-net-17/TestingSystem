using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth.Utilities;

public class RefreshTokenGenerator
{
    private const string Secret = "your-very-strong-refresh-secret-key"; // вынеси в настройки

    public static RefreshToken Generate(int userId)
    {
        var expiresAt = DateTime.UtcNow.AddDays(7);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-app",
            audience: "your-app-client",
            expires: expiresAt,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("type", "refresh")
            },
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new RefreshToken
        {
            UserId = userId,
            Token = tokenString,
            ExpiresAt = expiresAt,
            IsActive = true
        };
        
    }
}