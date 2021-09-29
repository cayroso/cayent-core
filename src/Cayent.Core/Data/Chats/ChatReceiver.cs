using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Data.Chats
{
    internal class ChatReceiver
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ChatReceiverId { get; set; }

        public string ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public string ReceiverId { get; set; }
        public virtual UserBase Receiver { get; set; }

        public string LastChatMessageId { get; set; }
        public bool IsRemoved { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }

    internal class ChatReceiverConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<ChatReceiver>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ChatReceiver> b)
        {
            b.ToTable("ChatReceiver");
            b.HasKey(e => e.ChatReceiverId);

            b.Property(e => e.ChatReceiverId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ChatId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ReceiverId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.LastChatMessageId).HasMaxLength(KeyMaxLength);
        }
    }
}
