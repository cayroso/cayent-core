﻿//using Cayent.Core.Data.Identity.Models;

namespace Cayent.Core.Data.Providers
{
    //public interface ITenantProvider
    //{
    //    Tenant GetTenant();
    //}
    //public class DummyTenantProvider : ITenantProvider
    //{
    //    Tenant _tenant;

    //    public DummyTenantProvider(Tenant tenant)
    //    {
    //        _tenant = tenant;
    //    }
    //    public Tenant GetTenant()
    //    {
    //        return _tenant;
    //    }

    //}

    //public class DefaultTenantProvider : ITenantProvider
    //{
    //    string _tenantId;
    //    IdentityWebContext _webContext;

    //    public DefaultTenantProvider(IHttpContextAccessor accessor, IdentityWebContext webContext)
    //    {
    //        if (accessor != null && accessor.HttpContext != null)
    //        {
    //            _tenantId = accessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "TenantId")?.Value;
    //        }

    //        _webContext = webContext;
    //    }

    //    public Tenant GetTenant()
    //    {
    //        if (!string.IsNullOrWhiteSpace(_tenantId))
    //        {
    //            var tenant = _webContext
    //                .Tenants
    //                .Include(p => p.Users)
    //                .AsNoTracking()
    //                .FirstOrDefault(p => p.TenantId == _tenantId);

    //            return tenant;
    //        }

    //        return null;
    //    }
    //}
}
