using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class ScheduledDonation
{
    public Guid ScheduledDonationId { get; set; }

    public decimal Amount { get; set; }
    public ScheduledDonationFrequency Frequency { get; set; } = ScheduledDonationFrequency.Monthly;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ScheduledDonationStatus Status { get; set; } = ScheduledDonationStatus.Ongoing;

    public Guid DonorId { get; set; }
    public Donor Donor { get; set; } = null!;

    public Guid? CharityId { get; set; }
    public Charity? Charity { get; set; }

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
}
