using Cayent.Data.App.DbContext;
using Cayent.Data.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.Providers
{
    public interface IAppDbContextFactory
    {
        void Provision(Tenant tenant, List<ProvisionUserRole> users, bool isDevelopment);

        AppDbContext Get();

        string GenerateConnectionString(string serverName, string databaseName);
    }
}
