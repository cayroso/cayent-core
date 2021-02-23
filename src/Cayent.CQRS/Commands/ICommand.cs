using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Business transaction id
        /// </summary>
        string CorrelationId { get; }
        string TenantId { get; }
        string UserId { get; }

    }
}
