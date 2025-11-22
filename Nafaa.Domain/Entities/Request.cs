using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Request
{
    public Guid RequestId { get; set; }

    public RequestType Type { get; set; } = RequestType.None;
    public RequestStatus Status { get; set; } = RequestStatus.InProgress;

    public DateTime SubmissionDate { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? CompletionDate { get; set; }

    public string? DocumentationPath { get; set; }
    public string? Description { get; set; }

    public RequestCategory Category { get; set; } = RequestCategory.Normal;
    public decimal AmountRequested { get; set; }
    public decimal AmountApproved { get; set; }
    public string? ReviewNotes { get; set; }

    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;

    public ICollection<Staff> ReviewerStaff { get; set; } = new List<Staff>();
}