using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }

    // Unique username: email (for donors/staff) OR National ID (for recipients)
    public string UserName { get; set; } = null!;

    public UserRole Role { get; set; }

    // Auth-related
    public string Password { get; set; } = null!;

    // Profile
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int Age => (int)((DateTime.UtcNow - DateOfBirth).TotalDays / 365);

    public string PhoneNumber { get; set; } = null!;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public bool IsEmailVerified { get; set; } = false;

    // Navigation
    public Recipient? Recipient { get; set; }
    public Donor? Donor { get; set; }
    public PartnerStaff? PartnerStaff { get; set; }
    public CharityStaff? CharityStaff { get; set; }

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

}
