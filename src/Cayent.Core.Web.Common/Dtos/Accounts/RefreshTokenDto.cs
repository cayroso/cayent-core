using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Common.Dtos.Accounts
{
    public class RefreshTokenDto
    {
        public string TenantHost { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
