using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cayent.Core.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<Paged<T>> ToPagedItemsAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default) where T : class
        {
            var count = await source.CountAsync().ConfigureAwait(false);
            var collection = await source
                .AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Paged<T>(collection, count, pageIndex, pageSize);
        }

    }
}
