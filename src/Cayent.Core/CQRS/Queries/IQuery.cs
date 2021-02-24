using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Queries
{
    public interface IQuery<out TResponse> where TResponse : class// IResponse
    {
        string CorrelationId { get; }
        string TenantId { get; }
        string UserId { get; }
    }

}
