// Models/Email/EmailSettings.cs
namespace Nafaa.Api.Models.Email;

public class EmailSettings
{
    // Basic settings
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "Nafaa Platform";
    public bool IsEnabled { get; set; } = true;
    public string Provider { get; set; } = "Smtp";

    // SMTP settings
    public string SmtpHost { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 587;
    public bool UseSsl { get; set; } = true;
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;

    // Development/testing settings
    public bool LogToConsole { get; set; } = true;
    public string OverrideToEmail { get; set; } = string.Empty;
}