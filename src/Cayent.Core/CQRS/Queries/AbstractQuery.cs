﻿namespace Cayent.Core.CQRS.Queries
{
    public abstract class AbstractQuery<TResponse> : IQuery<TResponse> where TResponse : class
    {
        public string CorrelationId { get; }
        public string TenantId { get; }
        public string UserId { get; }

        public AbstractQuery(string correlationId)
            : this(correlationId, string.Empty, string.Empty)
        { }

        public AbstractQuery(string correlationId, string userId)
            : this(correlationId, string.Empty, userId)
        { }

        public AbstractQuery(string correlationId, string tenantId, string userId)
        {
            CorrelationId = correlationId;
            TenantId = tenantId;
            UserId = userId;
        }

    }
}
