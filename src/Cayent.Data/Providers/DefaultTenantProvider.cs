using Cayent.Data.Identity.DbContext;
using Cayent.Data.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.Providers
{
    public class DefaultTenantProvider : ITenantProvider
    {
        string _tenantId;
        IdentityWebContext _webContext;

        public DefaultTenantProvider(IHttpContextAccessor accessor, IdentityWebContext webContext)
        {
            if (accessor != null && accessor.HttpContext != null)
            {
                _tenantId = accessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "TenantId")?.Value;
            }

            _webContext = webContext;
        }

        public Tenant GetTenant()
        {
            if (!string.IsNullOrWhiteSpace(_tenantId))
            {
                var tenant = _webContext
                    .Tenants
                    .Include(p => p.Users)
                    .AsNoTracking()
                    .FirstOrDefault(p => p.TenantId == _tenantId);

                return tenant;
            }

            return null;
        }
    }
}
