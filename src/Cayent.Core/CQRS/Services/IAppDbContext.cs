using Cayent.Core.Data.Calendars;
using Cayent.Core.Data.Chats;
using Cayent.Core.Data.Fileuploads;
using Cayent.Core.Data.Notifications;
using Cayent.Core.Data.Users;
using Data.Components.BranchStores;
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
        protected const int KeyMaxLength = 36;
        protected const int NameMaxLength = 256;
        protected const int DescMaxLength = 2048;
        protected const int NoteMaxLength = 4096;

        public AppBaseDbContext(DbContextOptions<AppBaseDbContext> options)
            : base(options)
        {

        }

        // https://github.com/aspnet/EntityFramework.Docs/issues/594
        protected AppBaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        
        public DbSet<UserBase> Users { get; set; }
        public DbSet<RoleBase> Roles { get; set; }
        public DbSet<UserRoleBase> UserRoles { get; set; }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        

        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatReceiver> ChatReceivers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreateCalendar(modelBuilder);

            CreateFileUploads(modelBuilder);

        }

        static void CreateCalendar(ModelBuilder builder)
        {
            builder.Entity<Calendar>(b =>
            {                
                b.ToTable("Calendar");
                b.HasKey(e => e.Date);

                b.Property(e => e.MonthName).HasMaxLength(KeyMaxLength).IsRequired();
                b.Property(e => e.DayName).HasMaxLength(KeyMaxLength).IsRequired();
            });

        }

        static void CreateFileUploads(ModelBuilder builder)
        {
            builder.Entity<FileUpload>(b =>
            {
                b.ToTable("FileUpload");
                b.HasKey(e => e.FileUploadId);

                b.Property(e => e.FileUploadId).HasMaxLength(KeyMaxLength).IsRequired();
                b.Property(e => e.Url).HasMaxLength(DescMaxLength);
                b.Property(e => e.FileName).HasMaxLength(DescMaxLength);
                b.Property(e => e.ContentDisposition).HasMaxLength(DescMaxLength);
                b.Property(e => e.ContentType).HasMaxLength(DescMaxLength);
            });
        }
    }
}
