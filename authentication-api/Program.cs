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
            var scope = host.Services.CreateScope();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            roleManager.CreateAsync(new IdentityRole { Name = "ADMIN" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole { Name = "USER" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole { Name = "REVIEWING" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole { Name = "BLOCKED" }).GetAwaiter().GetResult();

            AddUser(host, "admin", "ADMIN", "admin", "admin", "OTHER", "+32466550935",
                    "10/07/1995", "0");

            AddUser(host, "mohamadjarmak@gmail.com", "USER", "Mohamad", "Jarmak", "MALE", "+32466550935",
                "07/06/1995", "1");

            AddUser(host, "bigi_admin@businessmanager.com", "USER", "Francesco", "Bigi", "MALE", "+32466550935",
                "10/07/1995", "0");

            AddUser(host, "newbreaker@gmail.com", "REVIEWING", "Francesco", "Bigi", "MALE", "+32466550935",
                "10/07/1995", "0");

            AddUser(host, "blocked@gmail.com", "BLOCKED", "Pluto", "Pippo", "OTHER", "+32466550935",
                "10/07/1995", "0");

            host.Run();
        }

        public static void AddUser(IHost host, string email, string role, string name, string familyName, string gender, string phoneNumber, string birthdate, string professional)
        {
            var scope = host.Services.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            var user = new IdentityUser(email);
            userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, role);
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, email));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, name));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, familyName));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, gender));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, phoneNumber));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, birthdate));
            userManager.AddClaimAsync(user, new Claim("Professional", professional));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
