using Cayent.Core.Web.Client.RCL.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Components
{
    /// <summary>
    /// Base Component With Http Interceptor, Inheriting the base Component With Cancellation Token
    /// </summary>
    public abstract class BaseComponentWithHttpInterceptor : BaseComponentWithCancellationToken
    {
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Interceptor.RegisterEvent();
        }

        public override void Dispose()
        {
            Interceptor.DisposeEvent();

            base.Dispose();
        }
    }
}
