using IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace authentication_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                roleManager.CreateAsync(new IdentityRole { Name = "ADMIN" }).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole { Name = "USER" }).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole { Name = "REVIEWING" }).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole { Name = "BLOCKED" }).GetAwaiter().GetResult();

                var user = new IdentityUser("admin");
                userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, "ADMIN");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
