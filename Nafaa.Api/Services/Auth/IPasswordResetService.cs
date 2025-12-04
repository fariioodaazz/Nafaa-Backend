using Nafaa.Api.Models.Auth;

namespace Nafaa.Api.Services.Auth;

public interface IPasswordResetService
{
    /// <summary>
    /// Generates a reset token (if user exists), sends email using IEmailService
    /// and returns a debug reset link (for dev/testing only).
    /// </summary>
    Task<string?> SendResetPasswordAsync(string email);

    /// <summary>
    /// Resets the password if user + token are valid.
    /// Returns (Success, Errors).
    /// </summary>
    Task<(bool Success, IEnumerable<string>? Errors)> ResetPasswordAsync(
        string email,
        string token,
        string newPassword);
}
