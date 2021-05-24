using Cayent.Core.Data.Calendars;
using Cayent.Core.Data.Chats;
using Cayent.Core.Data.Fileuploads;
using Cayent.Core.Data.Notifications;
using Cayent.Core.Data.Components.Users;
using Microsoft.EntityFrameworkCore;


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


        //internal DbSet<UserBase> Users { get; set; }
        //internal DbSet<RoleBase> Roles { get; set; }
        //internal DbSet<UserRoleBase> UserRoles { get; set; }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        

        //internal DbSet<Chat> Chats { get; set; }
        //internal DbSet<ChatReceiver> ChatReceivers { get; set; }
        //internal DbSet<ChatMessage> ChatMessages { get; set; }

        //internal DbSet<Notification> Notifications { get; set; }
        //internal DbSet<NotificationReceiver> NotificationReceivers { get; set; }

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
