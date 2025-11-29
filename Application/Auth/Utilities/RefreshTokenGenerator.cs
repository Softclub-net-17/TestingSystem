using Domain.Entities;

namespace Application.Auth.Utilities;

public class RefreshTokenGenerator
{
    public static RefreshToken Generate(int userId)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString(), 
            ExpiresAt = DateTime.UtcNow.AddDays(7), 
            IsActive = true
        };
    }
}