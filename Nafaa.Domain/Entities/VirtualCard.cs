using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class VirtualCard
{
    public string VirtualCardCode { get; set; } = null!;

    public string CardHolderName { get; set; } = null!;
    public string? Cvv { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Pin { get; set; } = null!;
    public CardStatus CardStatus { get; set; }
    public int TotalPointsEarned { get; set; }

    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;

    public ICollection<Point> Points { get; set; } = new List<Point>();
}
