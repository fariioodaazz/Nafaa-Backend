namespace Nafaa.Domain.Entities;

public class CharityStaff : Staff
{
    public Guid CharityId { get; set; }
    public Charity Charity { get; set; } = null!;
    public ICollection<StaffPermission> StaffPermissions { get; set; } = new List<StaffPermission>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
