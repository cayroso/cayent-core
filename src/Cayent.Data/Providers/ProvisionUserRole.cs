using Cayent.Data.App.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.Providers
{
    public class ProvisionUserRole
    {
        public User User { get; set; }
        public List<string> RoleIds { get; set; } = new List<string>();

    }
}
