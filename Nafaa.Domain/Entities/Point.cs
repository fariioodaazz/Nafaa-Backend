using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Point
{
    public Guid PointId { get; set; }

    public PartnerType PointType { get; set; }
    public decimal PointsAmount { get; set; }

    public string VirtualCardCode { get; set; } = null!;
    public VirtualCard VirtualCard { get; set; } = null!;

    public Guid CharityId { get; set; }
    public Charity Charity { get; set; } = null!;
}
