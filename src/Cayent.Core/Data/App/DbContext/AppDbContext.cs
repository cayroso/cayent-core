//using Data.App.Chats;
//using Data.App.Models.Calendars;
//using Data.App.Models.Clinics;
//using Data.App.Models.FileUploads;
//using Data.App.Models.Patients;
//using Data.App.Models.Users;
//using Data.Identity.Models.Notifications;
//using Data.Identity.Models.Security;
//using Data.Providers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Data.App.DbContext
//{
//    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            // Get environment	
//            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

//            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory());
//            Console.WriteLine($"environment: {environment}");
//            Console.WriteLine($"appSettingsPath: {appSettingsPath}");

//            // Build config	
//            IConfiguration config = new ConfigurationBuilder()
//                .SetBasePath(appSettingsPath)
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{environment}.json", optional: true)
//                .AddEnvironmentVariables()
//                .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//            return new AppDbContext(optionsBuilder.Options, config, null);
//        }
//    }

//    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
//    {
//        #region configuration

//        bool _useSQLite;
//        string _connString;

//        #endregion


//        const int KeyMaxLength = 36;
//        const int NameMaxLength = 256;
//        const int DescMaxLength = 2048;
//        const int NoteMaxLength = 4096;

//        private Tenant _tenant;

//        private readonly IConfiguration _configuration;
//        private readonly ITenantProvider _tenantProvider;
//        public DbSet<Calendar> Calendars { get; set; }
//        public DbSet<FileUpload> FileUploads { get; set; }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Role> Roles { get; set; }
//        public DbSet<UserRole> UserRoles { get; set; }

//        public DbSet<Notification> Notifications { get; set; }
//        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }

//        public DbSet<Parent> Parents { get; set; }
//        public DbSet<Patient> Patients { get; set; }


//        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
//        {
//            builder.AddConsole();
//        });

//        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, ITenantProvider tenantProvider)
//            : base(options)
//        {
//            _configuration = configuration;
//            _tenantProvider = tenantProvider;

//            //if (tenantProvider != null)
//            //    _tenant = tenantProvider.GetTenantAsync().Result;

//            //_useSQLite = _configuration.GetValue<bool>("AppSettings:UseSQLite");

//            //_connString = _useSQLite ?
//            //    _configuration.GetConnectionString("AppDbContextConnectionSQLite")
//            //    : _configuration.GetConnectionString("AppDbContextConnectionSQLServer");

//            //if (_tenant != null)
//            //{
//            //    _connString = _tenant.DatabaseConnectionString;
//            //}

//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseLoggerFactory(_loggerFactory);
//#if DEBUG
//            optionsBuilder.EnableSensitiveDataLogging();
//#endif

//            if (!optionsBuilder.IsConfigured)
//            {
//                if (_tenantProvider != null)
//                    _tenant = _tenantProvider.GetTenantAsync().Result;

//                if (_useSQLite)
//                {
//                    _useSQLite = _configuration.GetValue<bool>("AppSettings:UseSQLite");

//                    _connString = _useSQLite ?
//                        _configuration.GetConnectionString("AppDbContextConnectionSQLite")
//                        : _configuration.GetConnectionString("AppDbContextConnectionSQLServer");

//                    optionsBuilder.UseSqlite(_connString);
//                }
//                else
//                {
//                    optionsBuilder.UseSqlServer(_connString);
//                }

//                if (_tenant != null)
//                {
//                    _connString = _tenant.DatabaseConnectionString;
//                }
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.ApplyConfiguration(new CalendarConfiguration());

//            builder.ApplyConfiguration(new ChatConfiguration());
//            builder.ApplyConfiguration(new ChatMessageConfiguration());
//            builder.ApplyConfiguration(new ChatReceiverConfiguration());

//            builder.ApplyConfiguration(new ClinicConfiguration());
//            builder.ApplyConfiguration(new ClinicReviewConfiguration());
//            builder.ApplyConfiguration(new ClinicScheduleConfiguration());
//            builder.ApplyConfiguration(new ClinicStaffConfiguration());
//            builder.ApplyConfiguration(new StaffConfiguration());

//            builder.ApplyConfiguration(new FileUploadConfiguration());

//            builder.ApplyConfiguration(new NotificationConfiguration());
//            builder.ApplyConfiguration(new NotificationReceiverConfiguration());

//            builder.ApplyConfiguration(new ParentConfiguration());
//            builder.ApplyConfiguration(new ParentClinicConfiguration());
//            builder.ApplyConfiguration(new PatientConfiguration());
//            builder.ApplyConfiguration(new PatientMedicalEntryConfiguration());

//            builder.ApplyConfiguration(new UserConfiguration());
//            builder.ApplyConfiguration(new RoleConfiguration());
//            builder.ApplyConfiguration(new UserRoleConfiguration());
//        }


//        void CreateCalendar(ModelBuilder builder)
//        {
//            builder.Entity<Calendar>(b =>
//            {
//                b.ToTable("Calendar");
//                b.HasKey(e => e.Date);

//                b.Property(e => e.MonthName).HasMaxLength(KeyMaxLength).IsRequired();
//                b.Property(e => e.DayName).HasMaxLength(KeyMaxLength).IsRequired();
//            });

//        }

//        void CreateFileUploads(ModelBuilder builder)
//        {
//            builder.Entity<FileUpload>(b =>
//            {
//                b.ToTable("FileUpload");
//                b.HasKey(e => e.FileUploadId);

//                b.Property(e => e.FileUploadId).HasMaxLength(KeyMaxLength).IsRequired();
//                b.Property(e => e.Url).HasMaxLength(DescMaxLength);
//                b.Property(e => e.FileName).HasMaxLength(DescMaxLength);
//                b.Property(e => e.ContentDisposition).HasMaxLength(DescMaxLength);
//                b.Property(e => e.ContentType).HasMaxLength(DescMaxLength);


//            });
//        }


//    }
//}
