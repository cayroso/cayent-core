using Cayent.Data.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.Providers
{
    public class DummyTenantProvider : ITenantProvider
    {
        Tenant _tenant;

        public DummyTenantProvider(Tenant tenant)
        {
            _tenant = tenant;
        }
        public Tenant GetTenant()
        {
            return _tenant;
        }

    }
}
