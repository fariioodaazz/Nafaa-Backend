using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nafaa.Domain.Entities;
using Nafaa.Infrastructure.Data;

namespace Nafaa.Api.Services.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly NafaaDbContext _dbContext;
    private readonly ILogger<RefreshTokenService> _logger;
    private readonly int _lifetimeDays;

    public RefreshTokenService(
        NafaaDbContext dbContext,
        IConfiguration configuration,
        ILogger<RefreshTokenService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _lifetimeDays = configuration.GetValue<int?>("Jwt:RefreshTokenLifetimeDays") ?? 7;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(User user, string? ipAddress)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(tokenBytes);

        var refreshToken = new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            Token = token,
            UserId = user.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(_lifetimeDays),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshToken;
    }

    public async Task<(bool Success, User? User, RefreshToken? Token, string? Error)> ValidateAsync(string token)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null)
            return (false, null, null, "Invalid refresh token.");

        if (refreshToken.IsRevoked)
            return (false, null, null, "Refresh token has been revoked.");

        if (refreshToken.ExpiresAt < DateTime.UtcNow)
            return (false, null, null, "Refresh token has expired.");

        return (true, refreshToken.User, refreshToken, null);
    }

    public async Task RevokeAsync(string token, string? ipAddress)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null)
            return;

        if (!refreshToken.IsRevoked)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RevokeAllForUserAsync(Guid userId, string? ipAddress)
    {
        var tokens = await _dbContext.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();

        foreach (var rt in tokens)
        {
            rt.IsRevoked = true;
            rt.RevokedAt = DateTime.UtcNow;
            rt.RevokedByIp = ipAddress;
        }

        await _dbContext.SaveChangesAsync();
    }
}
