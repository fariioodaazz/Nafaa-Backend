using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class FamilyMember
{
    public string NationalId { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public FamilyMemberStatus Status { get; set; } = FamilyMemberStatus.Alive;
    public FamilyMemberPermissions Permissions { get; set; } = FamilyMemberPermissions.Allowed;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.Single;
    public EducationLevel EducationLevel { get; set; } = EducationLevel.Illiterate;
    public MedicalHistory? MedicalHistory { get; set; }

    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;
}
