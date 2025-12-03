using Nafaa.Domain.Enums;

namespace Nafaa.Api.Models.Auth;

public class MeResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public UserRole Role { get; set; }

    public bool IsEmailVerified { get; set; }

    public Guid? DonorId { get; set; }
    public Guid? RecipientId { get; set; }
    public Guid? CharityStaffId { get; set; }
    public Guid? PartnerStaffId { get; set; }

    public string? Email { get; set; }
}
