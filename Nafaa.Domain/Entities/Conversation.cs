namespace Nafaa.Domain.Entities;

public class Conversation
{
    public Guid ConversationId { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid BotId { get; set; }
    public Chatbot Bot { get; set; } = null!;
    public DateTime LastMessageAt { get; set; } = DateTime.UtcNow;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}