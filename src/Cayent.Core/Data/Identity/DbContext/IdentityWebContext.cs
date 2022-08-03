
//using Data.Identity.Models;
//using Data.Identity.Models.Security;
//using Data.Identity.Models.Users;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Data.Identity.DbContext
//{
//    public class IdentityWebContext : IdentityDbContext<IdentityWebUser, IdentityRole, string, IdentityUserClaim<string>,
//        IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
//    {
//        #region configuration

//        readonly bool _useSQLite;
//        readonly string _connString;
//        readonly IConfiguration _configuration;

//        #endregion


//        const int KeyMaxLength = 36;
//        const int NameMaxLength = 256;
//        const int DescMaxLength = 2048;

//        public DbSet<Tenant> Tenants { get; set; }
//        public DbSet<UserInformation> UserInformations { get; set; }
//        public DbSet<LoginAudit> LoginAudits { get; set; }

//        //  app specific

//        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
//        {
//            builder.AddConsole();
//        });

//        public IdentityWebContext(DbContextOptions<IdentityWebContext> options, IConfiguration configuration)
//            : base(options)
//        {
//            _configuration = configuration;

//            _useSQLite = _configuration.GetValue<bool>("AppSettings:UseSQLite");

//            _connString = _useSQLite ?
//                _configuration.GetConnectionString("SystemDbContextConnectionSQLite")
//                : _configuration.GetConnectionString("SystemDbContextConnectionSQLServer");
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseLoggerFactory(_loggerFactory);
//#if DEBUG
//            optionsBuilder.EnableSensitiveDataLogging();
//#endif
//            if (!optionsBuilder.IsConfigured)
//            {
//_useSQLite = _configuration.GetValue<bool>("AppSettings:UseSQLite");

//            _connString = _useSQLite ?
//                _configuration.GetConnectionString("SystemDbContextConnectionSQLite")
//                : _configuration.GetConnectionString("SystemDbContextConnectionSQLServer");

//                if (_useSQLite)
//                {
//                    optionsBuilder.UseSqlite(_connString);
//                }
//                else
//                {
//                    optionsBuilder.UseSqlServer(_connString);
//                }
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//            // Customize the ASP.NET Identity model and override the defaults if needed.
//            // For example, you can rename the ASP.NET Identity table names and more.
//            // Add your customizations after calling base.OnModelCreating(builder);

//            builder.Entity<Feedback>(b =>
//            {
//                b.ToTable("Feedback");
//                b.HasKey(e => e.FeedbackId);

//            });

//            CreatingIdentity(builder);

//            builder.ApplyConfiguration(new TenantConfiguration());
//            builder.ApplyConfiguration(new TenantIdentityWebUserConfiguration());
//        }

//        void CreatingIdentity(ModelBuilder builder)
//        {
//            builder.ApplyConfiguration(new UserAddressConfiguration());
//            builder.ApplyConfiguration(new UserInformationConfiguration());
//            builder.ApplyConfiguration(new LoginAuditConfiguration());

//            builder.ApplyConfiguration(new IdentityWebUserConfiguration());

//            builder.Entity<IdentityRole>(b =>
//            {
//                b.ToTable("Role");

//                b.Property(e => e.Id).HasColumnName("RoleId").HasMaxLength(KeyMaxLength);
//                b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
//                b.HasIndex(e => e.Name).IsUnique();
//            });

//            builder.Entity<IdentityUserRole<string>>(b =>
//            {
//                b.ToTable("UserRole");

//                b.Property(e => e.UserId).HasMaxLength(KeyMaxLength);
//                b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength);

//            });

//            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
//            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
//            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
//            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");



//            //  app specific            
//        }
//    }
//}
