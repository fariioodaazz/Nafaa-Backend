namespace Nafaa.Api.Models.Auth;

public class LogoutRequest
{
    public string? RefreshToken { get; set; }
    public bool AllDevices { get; set; } = false;
}
