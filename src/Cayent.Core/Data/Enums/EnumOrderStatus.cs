using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Data.Enums
{
    public enum EnumOrderStatus
    {
        Unknown = 0,

        Placed,

        Accepted,
        Processed,
        Picked,
        Delivered,
        Completed,
        Cancelled,
        Returned,
    }
}
