using AuthorizationExample.Services.Contracts.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthorizationExample.Services.Authorization
{
    public class SignInManager<TUser> : Microsoft.AspNetCore.Identity.SignInManager<TUser>, ISignInManager<TUser>
        where TUser : class
    {
        public SignInManager(
            Microsoft.AspNetCore.Identity.UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<Microsoft.AspNetCore.Identity.SignInManager<TUser>> logger)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }
    }
}