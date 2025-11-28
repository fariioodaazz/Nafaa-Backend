namespace Nafaa.Domain.Entities;

public class CharityStaff : Staff
{
    public Guid CharityId { get; set; }
    public Charity Charity { get; set; } = null!;
    public ICollection<StaffPermission> StaffPermissions { get; set; } = new List<StaffPermission>();
    public ICollection<Request> ReviewedRequests { get; set; } = new List<Request>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
