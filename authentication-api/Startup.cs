using AspNet.Security.OpenIdConnect.Primitives;
using authentication_api.Context;
using business_manager_api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace authentication_api
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true; // for debugging

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44321/";
                    config.Audience = "auth";
                    config.Audience = "bm";
                    //config.RequireHttpsMetadata = false;
                });

            services.AddDbContext<DefaultContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            // AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<DefaultContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(AuthConfiguration.GetApis())
                .AddInMemoryIdentityResources(AuthConfiguration.GetIdentityResources())
                .AddInMemoryClients(AuthConfiguration.GetClients())
                .AddDeveloperSigningCredential();

            services.AddCors(config =>
                config.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                );
            services.AddControllers();

            //services.AddSwaggerGen(c => {
            //    c.SwaggerDoc("V1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "API EPHEC",
            //        Description = "Projet WEB - Mettre en place un webite publique avec authentication et rôles utilisant un backend composé d'api en .NET Core 3.1",
            //        TermsOfService = new Uri("https://example.com/terms"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Francesco Bigi",
            //            Email = "francesco.bigi.87@gmail.com",
            //            Url = new Uri("https://be.linkedin.com/in/bigif"),
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Use under LICX",
            //            Url = new Uri("https://example.com/license"),
            //        }
            //    });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger
            //app.UseSwagger(c =>
            //{
            //    c.SerializeAsV2 = true;
            //});

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            //Need to use an end point in order to access to swagger page
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/V1/swagger.json", "My API V1");
            //    c.RoutePrefix = string.Empty;
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
