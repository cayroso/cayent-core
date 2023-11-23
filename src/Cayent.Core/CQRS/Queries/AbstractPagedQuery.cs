﻿using Cayent.Core.Common;

namespace Cayent.Core.CQRS.Queries
{
    public abstract class AbstractPagedQuery<TResponse> : IQuery<Paged<TResponse>> where TResponse : class
    {
        public string CorrelationId { get; }
        public string TenantId { get; }
        public string UserId { get; }
        public string Criteria { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SortField { get; }
        public int SortOrder { get; }

        public AbstractPagedQuery(string correlationId, string criteria, int pageIndex, int pageSize, string sortField, int sortOrder)
            : this(correlationId, string.Empty, string.Empty, criteria, pageIndex, pageSize, sortField, sortOrder)
        { }

        public AbstractPagedQuery(string correlationId, string userId, string criteria, int pageIndex, int pageSize, string sortField, int sortOrder)
           : this(correlationId, string.Empty, userId, criteria, pageIndex, pageSize, sortField, sortOrder)
        { }

        public AbstractPagedQuery(string correlationId, string tenantId, string userId, string criteria, int pageIndex, int pageSize, string sortField, int sortOrder)
        {
            CorrelationId = correlationId;
            TenantId = tenantId;
            UserId = userId;
            Criteria = criteria;
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortField = sortField;
            SortOrder = sortOrder;
        }

    }
}
