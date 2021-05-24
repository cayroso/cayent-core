using System;
using System.Collections.Generic;

namespace Cayent.Core.Data.Chats
{
    internal class Chat
    {
        public string ChatId { get; set; }

        public string LastChatMessageId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ChatReceiver> Receivers { get; set; } = new List<ChatReceiver>();
        public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
