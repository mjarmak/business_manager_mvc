using authentication_api.Data;
using business_manager_api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.EntityFrameworkCore;
using System;

namespace authentication_api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("STARTING AUTH");
            IdentityModelEventSource.ShowPII = true;

            // AddIdentity registers the services
            //services.AddIdentity<IdentityUser, IdentityRole>(config =>
            //{
            //    config.Password.RequiredLength = 4;
            //    config.Password.RequireDigit = false;
            //    config.Password.RequireNonAlphanumeric = false;
            //    config.Password.RequireUppercase = false;
            //})
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();

            //services.ConfigureApplicationCookie(config =>
            //{
            //    config.Cookie.Name = "IdentityServer.Cookie";
            //    config.LoginPath = "/Auth/Login";
            //    config.LogoutPath = "/Auth/Logout";
            //});

            //var assembly = typeof(Startup).Assembly.GetName().Name;

            services.AddIdentityServer()
                //.AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(AuthConfiguration.GetApis())
                .AddInMemoryClients(AuthConfiguration.GetClients())
                //.AddInMemoryApiScopes(AuthConfiguration.GetScopes())
                //.AddInMemoryIdentityResources(AuthConfiguration.GetIdentityResources())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
