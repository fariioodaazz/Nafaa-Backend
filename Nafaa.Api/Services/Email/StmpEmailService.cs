// Services/Email/SmtpEmailService.cs
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Nafaa.Api.Models.Email;

namespace Nafaa.Api.Services.Email;

public class SmtpEmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<SmtpEmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        if (!_emailSettings.IsEnabled)
        {
            _logger.LogWarning("Email service is disabled.");
            return;
        }

        try
        {
            // Log to console for debugging
            if (_emailSettings.LogToConsole)
            {
                _logger.LogInformation("=== SMTP EMAIL READY TO SEND ===");
                _logger.LogInformation($"To: {toEmail}");
                _logger.LogInformation($"Subject: {subject}");
                _logger.LogInformation($"Content preview: {htmlContent.Substring(0, Math.Min(100, htmlContent.Length))}...");
            }

            using var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlContent;
            mailMessage.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort);
            smtpClient.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
            smtpClient.EnableSsl = _emailSettings.UseSsl;
            smtpClient.Timeout = 60000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtpClient.SendMailAsync(mailMessage);

            _logger.LogInformation($"✓ Email sent successfully to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to {toEmail}");

            // Fallback to console logging if SMTP fails
            _logger.LogWarning("Falling back to console logging...");
            _logger.LogInformation($"To: {toEmail}");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"HTML Content: {htmlContent}");

            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }

    public async Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink)
    {
        var subject = "Confirm Your Email - Nafaa Platform";
        var htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='UTF-8'>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .button {{ display: inline-block; padding: 12px 24px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                    .footer {{ margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 12px; }}
                </style>
            </head>
            <body>
                <div class='header'>
                    <h1>Welcome to Nafaa Platform!</h1>
                </div>
                <div class='content'>
                    <h2>Hello {userName},</h2>
                    <p>Thank you for registering with Nafaa Platform. To complete your registration, please confirm your email address by clicking the button below:</p>
                    
                    <div style='text-align: center;'>
                        <a href='{confirmationLink}' class='button'>Confirm Email Address</a>
                    </div>
                    
                    <p>Or copy and paste this link into your browser:</p>
                    <p style='word-break: break-all; color: #4CAF50; background-color: #f0f0f0; padding: 10px; border-radius: 3px;'>{confirmationLink}</p>
                    
                    <p>This link will expire in 24 hours.</p>
                    
                    <p>If you didn't create an account with Nafaa Platform, please ignore this email.</p>
                    
                    <p>Best regards,<br>The Nafaa Team</p>
                </div>
                <div class='footer'>
                    <p>© {DateTime.UtcNow.Year} Nafaa Platform. All rights reserved.</p>
                    <p>This email was sent to {toEmail}</p>
                </div>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, htmlContent);
    }


    public bool IsEmailServiceConfigured()
    {
        if (!_emailSettings.IsEnabled || _emailSettings.Provider != "Smtp")
            return false;

        return !string.IsNullOrEmpty(_emailSettings.SmtpHost) &&
               _emailSettings.SmtpPort > 0 &&
               !string.IsNullOrEmpty(_emailSettings.SmtpUsername) &&
               !string.IsNullOrEmpty(_emailSettings.SmtpPassword) &&
               !string.IsNullOrEmpty(_emailSettings.FromEmail);
    }
}