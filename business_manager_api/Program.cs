using business_manager_api.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace business_manager_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            AddBusinesses(host);

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void AddBusinesses(IHost host)
        {
            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Belgium", City = "Ixelles" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Belgium", City = "Auderghem" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Lebanon", City = "Beirut" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData(),
                BusinessInfo = new BusinessInfoData()
            });
            context.SaveChangesAsync();
        }
    }
}
