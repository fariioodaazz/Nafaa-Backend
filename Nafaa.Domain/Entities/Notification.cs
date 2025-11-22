using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Notification
{
    public Guid NotificationId { get; set; }

    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; } = NotificationType.Push;
    public DateTime SendTime { get; set; }

    public NotificationStatus Status { get; set; } = NotificationStatus.Unread;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
