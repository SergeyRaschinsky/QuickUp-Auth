using System;
using System.Text;
using AuthorizationExample.Auth.Middlewares.Authentication;
using AuthorizationExample.Auth.Models.Authentication;
using AuthorizationExample.Server.Common.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationExample.Server.Common.Extensions
{
    public static class StartupExtensions
    {
        public static void UseDefaultTokenProvider(this IApplicationBuilder app)
        {
            var authSettings = app.ApplicationServices.GetService<IOptions<AuthorizationSettings>>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Value.SecretKey));
            var options = new TokenProviderOptions
                          {
                              Audience = authSettings.Value.Audience,
                              Issuer = authSettings.Value.Issuer,
                              SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                              Expiration = TimeSpan.FromDays(authSettings.Value.ExpirationDays)
                          };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }
    }
}