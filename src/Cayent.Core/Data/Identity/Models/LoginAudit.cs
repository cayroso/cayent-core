using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Data.Identity.Models
{
    public class LoginAudit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LoginAuditId { get; set; }
        public string UserId { get; set; }
        public virtual IdentityWebUser User { get; set; }

        public string RemoteIpAddress { get; set; }

        DateTime _loginDate;
        public DateTime LoginDate
        {
            get => _loginDate;
            set => _loginDate = value.Truncate().AsUtc();
        }
    }
}
