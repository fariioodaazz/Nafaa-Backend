using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Project
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ProjectCategory Category { get; set; } = ProjectCategory.None;
    public decimal Balance { get; set; }
    public decimal TargetAmount { get; set; }

    public Guid CreatedByStaffId { get; set; }
    public Staff CreatedBy { get; set; } = null!;

    public DateTime CreationDate { get; set; }
    public int DurationDays { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;

    public Guid CharityId { get; set; }
    public Charity Charity { get; set; } = null!;

    public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    public ICollection<CharityStaff> StaffMembers { get; set; } = new List<CharityStaff>();


    public ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
