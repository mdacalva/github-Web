using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crypto.Api.Domain.User;
using Crypto.Api.Services;
using System.IO;
using System;
using Microsoft.AspNetCore.Authentication;
using Crypto.Api.Helpers;

namespace Crypto.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserContextService, UserContextService>();

            services.AddScoped<IPriceService, PriceService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // a temp folder is used to store user's preference 
            // to do replace with database call
            var folder = Path.Combine(AppContext.BaseDirectory, Configuration.GetSection("TemporaryDirectory")?.Value);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
