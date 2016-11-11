using AuthorizationExample.Data.Dal;
using AuthorizationExample.Data.Dal.Entities.Identity;
using AuthorizationExample.Server.Common.Extensions;
using AuthorizationExample.Server.Common.Models.Authentication;
using AuthorizationExample.Services.Authorization;
using AuthorizationExample.Services.Contracts.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace AuthorizationExample.Auth
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthorizationExampleDbContext>(
                options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AspNetUser, IdentityRole>(
                        options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireUppercase = false;
                            options.Password.RequiredLength = 3;
                            options.Password.RequireNonAlphanumeric = false;
                            options.User.RequireUniqueEmail = true;
                            options.Lockout.MaxFailedAccessAttempts = 5;
                        })
                    .AddEntityFrameworkStores<AuthorizationExampleDbContext>();

            services.Configure<AuthorizationSettings>(Configuration.GetSection("AuthorizationSettings"));

            RegisterDependencies(services);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultTokenProvider();

            app.UseMvc();
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.TryAddScoped<IUserManager<AspNetUser>, UserManager<AspNetUser>>();
            services.TryAddScoped<ISignInManager<AspNetUser>, SignInManager<AspNetUser>>();
        }
    }
}