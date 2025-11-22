using Nafaa.Domain.Enums;
namespace Nafaa.Domain.Entities;

public class Message
{
    public Guid MessageId { get; set; }

    public string Text { get; set; } = null!;
    public DateTime SendTime { get; set; }
    public MessageStatus Status { get; set; } = MessageStatus.Sent;
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;

    public bool IsFromUser { get; set; }              // true = user, false = chatbot
}
