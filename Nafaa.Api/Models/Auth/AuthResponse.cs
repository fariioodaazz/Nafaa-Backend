namespace Nafaa.Api.Models.Auth;

public class AuthResponse
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }

    public DateTime RefreshTokenExpiresAt { get; set; }
    public string RefreshToken { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public Guid? DonorId { get; set; }
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
}
