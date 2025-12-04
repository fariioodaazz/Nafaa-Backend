// Services/Email/ConsoleEmailService.cs
using Microsoft.Extensions.Options;
using Nafaa.Api.Models.Email;

namespace Nafaa.Api.Services.Email;

public class ConsoleEmailService : IEmailService
{
    private readonly ILogger<ConsoleEmailService> _logger;
    private readonly EmailSettings _emailSettings;

    public ConsoleEmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<ConsoleEmailService> logger)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }

    public Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        if (!_emailSettings.IsEnabled)
        {
            _logger.LogInformation("Email service is disabled.");
            return Task.CompletedTask;
        }

        _logger.LogInformation("=== CONSOLE EMAIL (NOT SENT) ===");
        _logger.LogInformation($"To: {toEmail}");
        _logger.LogInformation($"Subject: {subject}");
        _logger.LogInformation($"Content Preview: {htmlContent.Substring(0, Math.Min(100, htmlContent.Length))}...");

        return Task.CompletedTask;
    }

    public Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink)
    {
        var subject = "Confirm Your Email - Nafaa Platform";
        var htmlContent = $@"
            <h2>Email Confirmation</h2>
            <p>Hello {userName},</p>
            <p>Please confirm your email by clicking this link:</p>
            <p><a href='{confirmationLink}'>{confirmationLink}</a></p>";

        return SendEmailAsync(toEmail, subject, htmlContent);
    }


    public bool IsEmailServiceConfigured()
    {
        return _emailSettings.IsEnabled;
    }
}