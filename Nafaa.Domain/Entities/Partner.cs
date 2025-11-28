using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Partner
{
    public Guid PartnerId { get; set; }

    public string BankAccountNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Balance { get; set; }
    public PartnerType PartnerType { get; set; } = PartnerType.Other;

}
