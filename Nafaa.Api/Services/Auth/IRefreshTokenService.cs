using Nafaa.Domain.Entities;

namespace Nafaa.Api.Services.Auth;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(User user, string? ipAddress);
    Task<(bool Success, User? User, RefreshToken? Token, string? Error)> ValidateAsync(string token);
    Task RevokeAsync(string token, string? ipAddress);
    Task RevokeAllForUserAsync(Guid userId, string? ipAddress);
}
