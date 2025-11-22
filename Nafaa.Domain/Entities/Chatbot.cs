namespace Nafaa.Domain.Entities;

public class Chatbot
{
    public Guid BotId { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}
