namespace Nafaa.Domain.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    public string Token { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedByIp { get; set; }

    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }

    public bool IsRevoked { get; set; }

    public bool IsActive => !IsRevoked && DateTime.UtcNow <= ExpiresAt;
}
