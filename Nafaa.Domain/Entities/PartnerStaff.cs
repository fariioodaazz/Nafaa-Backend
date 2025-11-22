namespace Nafaa.Domain.Entities;

public class PartnerStaff : Staff
{
    public Guid PartnerId { get; set; }
    public Partner Partner { get; set; } = null!;
}
