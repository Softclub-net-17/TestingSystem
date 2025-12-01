using System.Text.Json.Serialization;

namespace Application.Auth.DTOs;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;
    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;
}