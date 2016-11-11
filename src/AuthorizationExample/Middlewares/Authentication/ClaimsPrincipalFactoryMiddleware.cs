using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuthorizationExample.Middlewares.Authentication
{
    public class ClaimsPrincipalFactoryMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsPrincipalFactoryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
                return _next.Invoke(context);

            var token = context.Request.Headers["Authorization"].ToString();

            var st = new JwtSecurityToken(token.Replace("Bearer ", ""));

            ((ClaimsIdentity) context.User.Identity).AddClaims(st.Claims);
            
            return _next.Invoke(context);
        }
    }
}
