using System;

namespace Cayent.Core.Common.Extensions
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
