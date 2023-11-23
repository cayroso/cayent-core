using Microsoft.EntityFrameworkCore;

namespace Cayent.Core.Data.Chats
{
    internal class Chat
    {
        public string ChatId { get; set; }

        public string LastChatMessageId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ChatReceiver> Receivers { get; set; } = new List<ChatReceiver>();
        public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }

    internal class ChatConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<Chat>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Chat> b)
        {
            b.ToTable("Chat");
            b.HasKey(e => e.ChatId);

            b.Property(e => e.ChatId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.LastChatMessageId).HasMaxLength(KeyMaxLength);

            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

        }
    }
}
