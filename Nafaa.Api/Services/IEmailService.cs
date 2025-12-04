// Services/IEmailService.cs
namespace Nafaa.Api.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink);
    bool IsEmailServiceConfigured();
}