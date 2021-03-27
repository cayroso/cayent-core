using Cayent.Core.Data.Chats;
using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cayent.Core.CQRS.Services
{
    public interface IAppDbContext
    {        
        DbSet<Chat> Chats { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<ChatReceiver> ChatReceivers { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNullAttribute] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task AddRangeAsync([NotNullAttribute] IEnumerable<object> entities, CancellationToken cancellationToken = default);
    }
}
