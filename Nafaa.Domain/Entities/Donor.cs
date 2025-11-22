using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Donor
{
    public Guid DonorId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    public ICollection<ScheduledDonation> ScheduledDonations { get; set; } = new List<ScheduledDonation>();
    public ICollection<CardInformation> Cards { get; set; } = new List<CardInformation>();

    // Favourites & endorsements: many-to-many with Charity
    public ICollection<Charity> FavoriteCharities { get; set; } = new List<Charity>();
    public ICollection<Charity> EndorsedCharities { get; set; } = new List<Charity>();
}
