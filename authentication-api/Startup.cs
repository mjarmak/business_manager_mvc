using business_manager_api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;

namespace authentication_api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("STARTING AUTH");
            IdentityModelEventSource.ShowPII = true;

            services.AddIdentityServer()
                .AddInMemoryApiResources(AuthConfiguration.GetApis())
                .AddInMemoryClients(AuthConfiguration.GetClients())
                //.AddInMemoryApiScopes(AuthConfiguration.GetScopes())
                .AddDeveloperSigningCredential();

            services.AddCors(confg =>
                confg.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                );

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
