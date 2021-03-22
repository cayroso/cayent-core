using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        public AbstractCommand(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public AbstractCommand(string correlationId, string userId)
        {
            CorrelationId = correlationId;            
            UserId = userId;
        }

        public AbstractCommand(string correlationId, string tenantId, string userId)
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
