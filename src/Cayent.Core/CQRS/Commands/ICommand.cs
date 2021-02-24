using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Commands
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
