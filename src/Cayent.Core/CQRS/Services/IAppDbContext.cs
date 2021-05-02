using Cayent.Core.Data.Chats;
using Cayent.Core.Data.Fileuploads;
using Cayent.Core.Data.Notifications;
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
    public abstract class AppBaseDbContext: DbContext
    {
        public AppBaseDbContext(DbContextOptions<AppBaseDbContext> options)
            : base(options)
        {

        }

        // https://github.com/aspnet/EntityFramework.Docs/issues/594
        protected AppBaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserBase> Users { get; set; }
        public DbSet<RoleBase> Roles { get; set; }
        public DbSet<UserRoleBase> UserRoles { get; set; }
        public DbSet<ChatReceiver> ChatReceivers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
