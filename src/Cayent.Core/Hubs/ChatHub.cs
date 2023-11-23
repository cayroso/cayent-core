using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cayent.Core.Hubs
{
    public interface IChatClient
    {
        Task ChatMessageReceived(ChatMessageReceivedInfo info);

        Task ChatMessageReceivedFromGroup(ChatMessageReceivedInfo info);

        Task ChatReceiverAdded(ChatReceiverAddedInfo info);

        Task ChatReceiverRemoved(ChatReceiverRemovedInfo info);

        Task ChatDeleted(string chatId);
    }

    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task LeaveChat(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }

    public class ChatMessageReceivedInfo
    {
        public string ChatMessageId { get; set; }
        public string ChatId { get; set; }
        public int ChatMessageType { get; set; }
        public SenderInfo Sender { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }

        public class SenderInfo
        {
            public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Initials { get; set; }
            public string ProfilePicture32 { get; set; }
        }
    }

    public class ChatReceiverAddedInfo
    {

    }

    public class ChatReceiverRemovedInfo
    {

    }
}
