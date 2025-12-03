namespace Nafaa.Api.Models.Auth;

public class DonorLoginRequest
{
    public string Identifier { get; set; } = null!;
    public string Password { get; set; } = null!;
}
