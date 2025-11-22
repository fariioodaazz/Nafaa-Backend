using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Donation
{
    public Guid DonationId { get; set; }

    public decimal Amount { get; set; }

    public DonationType DonationType { get; set; } = DonationType.Money;
    public DateTime DonationDate { get; set; }

    public DonationStatus Status { get; set; } = DonationStatus.InProgress;

    public Guid DonorId { get; set; }
    public Donor Donor { get; set; } = null!;

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }

    public Payment Payment { get; set; } = null!;
}
