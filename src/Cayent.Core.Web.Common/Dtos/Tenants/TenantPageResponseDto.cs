using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Common.Dtos.Tenants
{
    public class TenantPageResponseDto
    {
        public string TenantId { get; set; }
        public string Layout { get; set; }
        public string Header { get; set; }
        public string SidebarLeft { get; set; }
        public string SidebarRight { get; set; }
        public string Content { get; set; }
        public string Footer { get; set; }
    }
}
