using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Housing
{
    public Guid HouseId { get; set; }

    public HouseType Type { get; set; } = HouseType.Apartment;
    public decimal Size { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfFloors { get; set; }

    public HouseOwnership Ownership { get; set; } = HouseOwnership.Rent;
    public decimal? RentAmount { get; set; }
    public HouseIndependence Independence { get; set; } = HouseIndependence.Shared;

    public HouseWaterSource WaterSource { get; set; } = HouseWaterSource.PublicSystem;
    public bool HasElectricity { get; set; }
    public string? QualityDescription { get; set; }

    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;
}
