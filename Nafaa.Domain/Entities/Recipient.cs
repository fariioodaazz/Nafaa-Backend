using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Recipient
{
    public Guid RecipientId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid CharityId { get; set; }
    public Charity Charity { get; set; } = null!;

    // Basic info
    public string Address { get; set; } = null!;  // We'll keep it simple string for now
    public string Job { get; set; } = null!;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.Single;
    public EducationLevel EducationLevel { get; set; } = EducationLevel.Illiterate;
    public string BankAccountNumber { get; set; } = null!;
    public string WalletNumber { get; set; } = null!;

    public EmploymentStatus EmploymentStatus { get; set; } = EmploymentStatus.Unemployed;
    public decimal MonthlyIncome { get; set; }
    public decimal MonthlyAssistance { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    // 1 Recipient -> 1 VirtualCard
    public VirtualCard VirtualCard { get; set; } = null!;

    // 1 Recipient -> many FamilyMembers
    public ICollection<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();

    // 1 Recipient -> 1 Housing
    public Housing? Housing { get; set; }

    // 1 Recipient -> many MedicalHistory records
    public ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    // 1 Recipient -> many Requests
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}

