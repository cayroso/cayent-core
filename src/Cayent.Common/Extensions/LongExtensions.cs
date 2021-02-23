using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Common.Extensions
{
    public static class LongExtensions
    {
        public static DateTime ToUtcDate(this long ticks)
        {
            var date = DateTime.MinValue;

            if (ticks > 0)
                date = DateTimeOffset.FromUnixTimeMilliseconds(ticks).UtcDateTime;

            return date;
        }
    }
}
