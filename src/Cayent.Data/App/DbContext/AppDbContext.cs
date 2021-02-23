using Cayent.Data.Identity.Models;
using Cayent.Data.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.App.DbContext
{
    public abstract class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region configuration

        readonly bool _useSQLite;
        readonly string _connString;

        #endregion


        const int KeyMaxLength = 36;
        const int NameMaxLength = 256;
        const int DescMaxLength = 2048;
        const int NoteMaxLength = 4096;

        private Tenant _tenant;

        private readonly IConfiguration _configuration;


        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, ITenantProvider tenantProvider)
            : base(options)
        {
            _configuration = configuration;

            if (tenantProvider != null)
                _tenant = tenantProvider.GetTenant();


            _useSQLite = _configuration.GetValue<bool>("AppSettings:UseSQLite");

            _connString = _useSQLite ? _configuration.GetConnectionString("AppDbContextConnectionSQLite") : _configuration.GetConnectionString("AppDbContextConnectionSQLServer");

            if (_tenant != null)
            {
                _connString = _tenant.DatabaseConnectionString;
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useSQLite)
            {
                optionsBuilder.UseSqlite(_connString);
            }
            else
            {
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //}

    }
}
