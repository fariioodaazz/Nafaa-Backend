using Nafaa.Domain.Enums;

namespace Nafaa.Domain.Entities;

public abstract class Staff
{
    public Guid StaffId { get; set; }

    // Every staff member is also a User
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ContractType ContractType { get; set; }
    public decimal Wage { get; set; }
    public int WeeklyWorkingHours { get; set; }

    public StaffStatus Status { get; set; } = StaffStatus.Active;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
