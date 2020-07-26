using business_manager_api.Context;
using business_manager_common_library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

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
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 }
                },
                Identification = new IdentificationData { Type = BusinessTypeEnum.CLUB.ToString() },
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Belgium", City = "Ixelles" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 }
                },
                Identification = new IdentificationData { Type = BusinessTypeEnum.BAR.ToString() },
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Belgium", City = "Auderghem" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData { Type = BusinessTypeEnum.BAR.ToString() },
                BusinessInfo = new BusinessInfoData { Address = new AddressData { Country = "Lebanon", City = "Beirut" } }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.BAR.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.BAR.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.CONCERT.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.CLUB.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.CONCERT.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.CONCERT.ToString() },
                BusinessInfo = new BusinessInfoData()
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = null,
                Identification = new IdentificationData { Type = BusinessTypeEnum.STUDENTCIRCLE.ToString() },
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
