namespace Nafaa.Domain.Entities;

public class Charity
{
    public Guid CharityId { get; set; }

    public Uuid RegistrationNumber { get; set; } = null!;
    public string CharityName { get; set; } = null!;
    public string Address { get; set; } = null!; 

    public string Description { get; set; } = null!;
    public string Aim { get; set; } = null!;
    public string Vision { get; set; } = null!;

    public string BankAccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }

    // Derived fields
    public int NumberOfRecipients { get; set; }
    public int NumberOfStaff { get; set; }
    public int NumberOfCampaigns { get; set; }
    public double EndorsementScore { get; set; }

    public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
    public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    public ICollection<Donor> EndorsingDonors { get; set; } = new List<Donor>();
    public ICollection<Donor> FavoriteDonors { get; set; } = new List<Donor>();
}
