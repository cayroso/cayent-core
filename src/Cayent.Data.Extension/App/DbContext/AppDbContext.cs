using Cayent.Data.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Data.Extension.App.DbContext
{
    public class MyAppDbContext : Cayent.Data.App.DbContext.AppDbContext
    {
        public MyAppDbContext(DbContextOptions<Cayent.Data.App.DbContext.AppDbContext> options, IConfiguration configuration, ITenantProvider tenantProvider)
            : base(options, configuration, tenantProvider)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
