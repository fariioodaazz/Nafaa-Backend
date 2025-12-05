namespace Nafaa.Api.Models.Auth;

using System.ComponentModel.DataAnnotations;
public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;

    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}