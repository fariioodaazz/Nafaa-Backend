using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Nafaa.Api.Models.Auth;
using Nafaa.Api.Services.Email;
using Nafaa.Infrastructure.Models; // ApplicationUser
using Microsoft.Extensions.Logging;
using Nafaa.Api.Services.Auth;

namespace Nafaa.Api.Services.Auth;

public class PasswordResetService : IPasswordResetService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILogger<PasswordResetService> _logger;
    private readonly string _frontendBaseUrl;

    public PasswordResetService(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        IConfiguration configuration,
        ILogger<PasswordResetService> logger)
    {
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;

        // Frontend URL for reset page – adjust to your real app later
        _frontendBaseUrl = configuration["Frontend:BaseUrl"]
                           ?? "http://localhost:5173"; // or whatever your frontend dev port is
    }

    public async Task<string?> SendResetPasswordAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser == null)
        {
            // We silently ignore to avoid user enumeration
            _logger.LogInformation("Forgot password requested for non-existing email {Email}", email);
            return null;
        }

        // Generate reset token
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);

        // Encode for URL
        var encodedToken = WebUtility.UrlEncode(resetToken);
        var encodedEmail = WebUtility.UrlEncode(email);

        // Build frontend reset link
        var resetLink =
            $"{_frontendBaseUrl}/reset-password?email={encodedEmail}&token={encodedToken}";

        var subject = "Reset Your Password - Nafaa Platform";
        var htmlContent = $@"
            <h2>Password Reset Request</h2>
            <p>Hello ,</p>
            <p>We received a request to reset the password for your Nafaa account.</p>
            <p>If you made this request, click the link below to reset your password:</p>
            <p><a href='{resetLink}'>Reset Password</a></p>
            <p>If you did not request this, you can safely ignore this email.</p>
        ";

        await _emailService.SendEmailAsync(email, subject, htmlContent);

        // Return link so YOU can see it in Postman/dev (frontend won’t use this)
        return resetLink;
    }

    public async Task<(bool Success, IEnumerable<string>? Errors)> ResetPasswordAsync(
        string email,
        string token,
        string newPassword)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser == null)
        {
            _logger.LogWarning("Reset password requested for non-existing email {Email}", email);
            // behave as if succeeded (no info leak)
            return (true, null);
        }

        var decodedToken = WebUtility.UrlDecode(token);

        var result = await _userManager.ResetPasswordAsync(identityUser, decodedToken, newPassword);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, null);
    }
}
