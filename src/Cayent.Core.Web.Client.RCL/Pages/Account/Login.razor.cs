
using Cayent.Core.Web.Client.RCL.Services;
using Cayent.Core.Web.Common.Dtos.Accounts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Pages.Account
{
    public partial class Login
    {
        private LoginDto _userForAuthentication = new LoginDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        [Parameter]
        public string ReturnUrl { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                var url = ReturnUrl ?? "/";

                NavigationManager.NavigateTo(url);
            }
        }
    }
}
