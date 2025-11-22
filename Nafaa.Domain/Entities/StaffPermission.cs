namespace Nafaa.Domain.Entities;

public class StaffPermission
{
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;
    public string PermissionDescription { get; set; } = null!;

    public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
}
