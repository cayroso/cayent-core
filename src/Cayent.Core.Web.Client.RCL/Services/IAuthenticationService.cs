
using Cayent.Core.Web.Common.Dtos.Accounts;
using Cayent.Core.Web.Common.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResponseDto> RegisterUser(RegisterDto userForRegistration);
        Task<AddUserResponseDto> AddUser(AddUserDto addUserDto);

        Task<LoginResponseDto> Login(LoginDto userForAuthentication);
        Task Logout();
        Task<string> RefreshToken(string tenantHost);
    }
}
