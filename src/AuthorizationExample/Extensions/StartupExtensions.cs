using System;
using System.Text;
using AuthorizationExample.Server.Common.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationExample.Extensions
{
    public static class StartupExtensions
    {
        public static void UseTokenBasedAuth(this IApplicationBuilder app)
        {
            var authSettings = app.ApplicationServices.GetService<IOptions<AuthorizationSettings>>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Value.SecretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = authSettings.Value.Issuer,
                ValidateAudience = true,
                ValidAudience = authSettings.Value.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(
                new JwtBearerOptions
                {
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    TokenValidationParameters = tokenValidationParameters,
                });
        }
    }
}