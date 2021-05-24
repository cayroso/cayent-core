using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Components.Users;
using System;

namespace Cayent.Core.Data.Chats
{
    public enum EnumChatMessageType
    {
        System = 0,
        User = 1
    }

    internal class ChatMessage
    {
        public string ChatMessageId { get; set; }

        public string ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public string SenderId { get; set; }
        public virtual UserBase Sender { get; set; }

        public string Content { get; set; }

        public EnumChatMessageType ChatMessageType { get; set; } = EnumChatMessageType.User;

        DateTime _dateSent;
        public DateTime DateSent
        {
            get => _dateSent.AsUtc();
            set => _dateSent = value.Truncate();
        }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
