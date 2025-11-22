using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public string? PaymentGatewayReference { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Successful;
    public decimal Fee { get; set; }

    public Guid DonationId { get; set; }
    public Donation Donation { get; set; } = null!;
}
