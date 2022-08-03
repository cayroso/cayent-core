using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Components
{
    /// <summary>
    /// Base Component with Cancellation Token
    /// </summary>
    public abstract class BaseComponentWithCancellationToken : ComponentBase, IDisposable
    {
        protected CancellationTokenSource? CancellationTokenSource;

        protected CancellationToken CancellationToken => (CancellationTokenSource ??= new()).Token;

        public virtual void Dispose()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
                CancellationTokenSource.Dispose();
                CancellationTokenSource = null;
            }
        }
    }
}
