using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Queries
{
    public abstract class AbstractQuery<TResponse> : IQuery<TResponse> where TResponse : class
    {
        public AbstractQuery(string correlationId, string tenantId, string userId)
        {
            CorrelationId = correlationId;
            TenantId = tenantId;
            UserId = userId;
        }

        public string CorrelationId { get; }
        public string TenantId { get; }
        public string UserId { get; }
    }
}
