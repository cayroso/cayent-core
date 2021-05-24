
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Commands;
using System.Threading;
using Cayent.Core.CQRS.Common.Chats.Commands.Command;
using Cayent.Core.CQRS.Services;
using Microsoft.AspNetCore.SignalR;
using Cayent.Core.Hubs;
using Cayent.Core.Data.Chats;
using Microsoft.EntityFrameworkCore;

namespace Cayent.Core.CQRS.Common.Chats.Commands.Handler
{
    //public sealed class ChatCommandHandler :
    //    ICommandHandler<AddChatCommand>,
    //    ICommandHandler<AddChatMessageCommand>
    //{
    //    private readonly AppBaseDbContext _dbContext;
    //    private readonly IHubContext<ChatHub, IChatClient> _hubContext;

    //    public ChatCommandHandler(AppBaseDbContext dbContext, IHubContext<ChatHub, IChatClient> hubContext)
    //    {
    //        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    //        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    //    }

    //    async Task ICommandHandler<AddChatCommand>.HandleAsync(AddChatCommand command, CancellationToken cancellationToken)
    //    {
    //        //  check if already existing
    //        var exists = await _dbContext.Chats
    //            .Include(p => p.Receivers)
    //            .Where(p => p.Receivers.Count == 2
    //                    && p.Receivers.Any(q => q.ReceiverId == command.MemberId1)
    //                    && p.Receivers.Any(q => q.ReceiverId == command.MemberId2))
    //            .FirstOrDefaultAsync(cancellationToken);

    //        if (exists != null)
    //        {
    //            //  reactivate member2
    //            var member = exists.Receivers.First(p => p.ReceiverId == command.MemberId2);

    //            if (member.IsRemoved)
    //            {
    //                member.IsRemoved = false;

    //                await _dbContext.SaveChangesAsync(cancellationToken);

    //                var cmd1 = new AddChatMessageCommand(command.CorrelationId, command.TenantId, command.UserId, exists.ChatId, command.MemberId2, "Rejoining the chat.", EnumChatMessageType.System);

    //                await HandleAsync(cmd1, cancellationToken);
    //            }

    //            return;
    //        }

    //        var members = new[] { command.MemberId1, command.MemberId2 };

    //        var chat = new Chat
    //        {
    //            ChatId = command.ChatId,
    //            Title = await CreateChatTitle(members, cancellationToken),
    //            LastChatMessageId = string.Empty
    //        };

    //        var chatReceivers = members.Select(p =>
    //        {
    //            var info = new ChatReceiver
    //            {
    //                ChatReceiverId = Guid.NewGuid().ToString(),
    //                ChatId = chat.ChatId,
    //                ReceiverId = p,
    //                IsRemoved = false
    //            };

    //            return info;
    //        });

    //        //  chat
    //        await _dbContext.AddAsync(chat, cancellationToken);
    //        //  receivers
    //        await _dbContext.AddRangeAsync(chatReceivers, cancellationToken);
    //        //  save
    //        await _dbContext.SaveChangesAsync(cancellationToken);

    //        //  starting message
    //        var cmd2 = new AddChatMessageCommand(command.CorrelationId, command.TenantId, command.UserId, command.ChatId, command.MemberId1, "Joined the chat.", EnumChatMessageType.System);

    //        await HandleAsync(cmd2, cancellationToken);
    //    }

    //    public async Task HandleAsync(AddChatMessageCommand command, CancellationToken cancellationToken)
    //    {
    //        //var chat = await _dbContext
    //        //    .Chats
    //        //    .Include(p => p.Receivers)
    //        //    .Where(p => p.Receivers.Any(q => q.ReceiverId == command.SenderId))
    //        //    .FirstOrDefaultAsync(p => p.ChatId == command.ChatId, cancellationToken);

    //        //var now = DateTime.UtcNow;

    //        //var chatMessage = new ChatMessage
    //        //{
    //        //    ChatMessageId = Guid.NewGuid().ToString(),
    //        //    ChatId = command.ChatId,
    //        //    Content = command.Content,
    //        //    DateSent = now,
    //        //    SenderId = command.SenderId,
    //        //    ChatMessageType = command.ChatMessageType,
    //        //};

    //        //chat.LastChatMessageId = chatMessage.ChatMessageId;

    //        ////  this sender has read this message
    //        //chat.Receivers.First(p => p.ReceiverId == command.SenderId).LastChatMessageId = chatMessage.ChatMessageId;

    //        //await _dbContext.AddAsync(chatMessage);

    //        //await _dbContext.SaveChangesAsync(cancellationToken);

    //        //#region Send notifications

    //        //var sender = await _dbContext
    //        //    .Users
    //        //    .AsNoTracking()
    //        //    .FirstAsync(q => q.UserId == command.SenderId, cancellationToken);

    //        //var receivedMessageInfo = new ChatMessageReceivedInfo
    //        //{
    //        //    ChatId = chat.ChatId,
    //        //    ChatMessageId = chatMessage.ChatMessageId,
    //        //    ChatMessageType = (int)chatMessage.ChatMessageType,
    //        //    Content = chatMessage.Content,
    //        //    DateSent = chatMessage.DateSent,
    //        //    Sender = new ChatMessageReceivedInfo.SenderInfo
    //        //    {
    //        //        UserId = sender.UserId,
    //        //        Initials = $"{sender.FirstName[0]} {sender.LastName[0]}",
    //        //        FirstName = sender.FirstName,
    //        //        LastName = sender.LastName,
    //        //        ProfilePicture32 = sender.ImageId
    //        //    }
    //        //};

    //        ////  notify the other receivers, targets the navbar
    //        //await _hubContext.Clients.Users(chat.Receivers.Select(p => p.ReceiverId).ToArray()).ChatMessageReceived(receivedMessageInfo);

    //        ////  notify open chat clients
    //        //await _hubContext.Clients.Groups(chat.ChatId).ChatMessageReceivedFromGroup(receivedMessageInfo);

    //        //#endregion
    //        throw new NotImplementedException();
    //    }

    //    async Task<string> CreateChatTitle(string[] memberIds, CancellationToken cancellationToken)
    //    {
    //        //var users = await _dbContext.Users
    //        //            //.Include(e => e.Account)
    //        //            .Where(e => memberIds.Contains(e.UserId))
    //        //            .ToListAsync(cancellationToken);


    //        //var names = new List<string>();

    //        //users.ForEach(e =>
    //        //{
    //        //    var entry = e.FirstName;// $"{e.Id}/{e.Account.FirstName}";

    //        //    names.Add(entry);
    //        //});

    //        //var title = string.Join(", ", names.ToArray());

    //        //return title;

    //        throw new NotImplementedException();
    //    }
    //}
}
