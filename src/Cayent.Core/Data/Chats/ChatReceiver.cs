using Cayent.Core.Data.Components.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
}
