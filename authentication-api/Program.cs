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
                userManager.AddToRoleAsync(user, "USER");
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, "admin@businessmanager.com"));
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, "admin"));
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, "admin"));
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, "OTHER"));
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, "+32466550935"));
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, "10/07/2020 17:02:31"));
                userManager.AddClaimAsync(user, new Claim("Professional", "0"));
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
