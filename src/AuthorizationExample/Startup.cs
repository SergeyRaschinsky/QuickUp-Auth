using AuthorizationExample.Data.Dal;
using AuthorizationExample.Data.Dal.Entities.Identity;
using AuthorizationExample.Extensions;
using AuthorizationExample.Middlewares.Authentication;
using AuthorizationExample.Server.Common.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthorizationExample
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
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly("AuthorizationExample")));

            services.Configure<AuthorizationSettings>(Configuration.GetSection("AuthorizationSettings"));

            services.AddIdentity<AspNetUser, IdentityRole>()
                    .AddEntityFrameworkStores<AuthorizationExampleDbContext>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTokenBasedAuth();
            app.UseMiddleware<ClaimsPrincipalFactoryMiddleware>();

            app.UseMvc();
        }
    }
}