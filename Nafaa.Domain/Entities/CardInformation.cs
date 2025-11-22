namespace Nafaa.Domain.Entities;

public class CardInformation
{
    public string CardNumber { get; set; } = null!;
    public string CardHolder { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string Cvv { get; set; } = null!;

    public Guid DonorId { get; set; }
    public Donor Donor { get; set; } = null!;
}
