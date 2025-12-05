using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Recipient
{
    public Guid RecipientId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

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

    // many Recipients -> many Charities
    public ICollection<Charity> Charities { get; set; } = new List<Charity>();

    // 1 Recipient -> 1 VirtualCard
    public VirtualCard VirtualCard { get; set; } = null!;

    // 1 Recipient -> many FamilyMembers
    public ICollection<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();

    // 1 Recipient -> many Housing
    public ICollection<Housing> Housings { get; set; } = new List<Housing>();

    // 1 Recipient -> many MedicalHistory records
    public ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    // 1 Recipient -> many Requests
    public ICollection<Request> Requests { get; set; } = new List<Request>();

    // 1 Recipient -> many Projects
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}

