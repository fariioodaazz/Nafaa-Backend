using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class MedicalHistory
{
    public Guid MedicalHistoryId { get; set; }

    public DateTime RecordDate { get; set; }

    public Disability? Disability { get; set; }
    public ChronicCondition? ChronicCondition { get; set; }
    public List<string> Medications { get; set; } = new List<string>();
    public MobilityStatus? MobilityStatus { get; set; }
    public bool RequiresMedicalCare { get; set; }
    public string? Notes { get; set; }

    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;
}
