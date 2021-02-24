using Cayent.Data.Identity.DbContext;
using Cayent.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.App.DbContext
{
    public static class AppDbContextInitializer
    {
        static Random _rnd = new Random((int)DateTime.UtcNow.Ticks);

        public static void Initialize(IdentityWebContext identityWebContext, AppDbContext ctx, IEnumerable<ProvisionUserRole> provisionUserRoles)
        {
            //if (ctx.Users.Any())
            //    return;

            //var clinic = CreateClinic();

            //CreateRoles(ctx, clinic);

            //CopyIdentityUserToApp(identityWebContext, ctx, clinic);

            //ctx.Add(clinic);

            ctx.SaveChanges();
        }
    }
}
