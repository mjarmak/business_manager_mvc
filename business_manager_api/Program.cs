using business_manager_api.Context;
using business_manager_common_library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using business_manager_api.Domain;
// ReSharper disable All

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
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 }
                },
                
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CLUB.ToString(), 
                    Name = "Disco Club Belgium", 
                    EmailPro = "mohamadjarmak@gmail.com", 
                    Description = "Disco Club since 1990.",
                    LogoPath = "default-picture (1).jpeg"
                },
                BusinessInfo = new BusinessInfoData {
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData
                    {
                        City = "Bruxelles", 
                        Country = "Belgium", 
                        Street = "Rue de Belgique", 
                        BoxNumber = "1", 
                        PostalCode = "1100"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html", 
                    UrlFaceBook = "https://www.facebook.com/bigfra12", 
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (2).jpeg",
                    PhotoPath2 = "default-picture (3).jpeg",
                    PhotoPath3 = "default-picture (4).jpeg",
                    PhotoPath4 = "default-picture (5).jpeg",
                    PhotoPath5 = "default-picture (50).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 15, HourTo = 23, Closed = false, MinuteFrom = 0, MinuteTo = 0 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.BAR.ToString(), 
                    Name = "Disco Bar Italy", 
                    EmailPro = "francesco.bigi.87@gmail.com", 
                    Description = "Disco Bar Italy since 1990.",
                    LogoPath = "default-picture (60).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Bruxelles", 
                        Country = "Belgium", 
                        Street = "Rue de L'Italie", 
                        BoxNumber = "2", 
                        PostalCode = "1200"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html", 
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (62).jpeg",
                    PhotoPath2 = "default-picture (63).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.BAR.ToString(),
                    Name = "Disco Bar Beirut",
                    EmailPro = "francesco.bigi.87@gmail.com",
                    Description = "Disco Bar Beirut since 1990.",
                    LogoPath = "default-picture (5).jpeg"
                },
                BusinessInfo = new BusinessInfoData { Phone = "+32493741427", 
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Beirut",
                        Country = "Lebanon", 
                        Street = "Rue de France", 
                        BoxNumber = "3", 
                        PostalCode = "1300"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (6).jpeg",
                    PhotoPath2 = "default-picture (7).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData { 
                    Type = BusinessTypeEnum.BAR.ToString(),
                    Name = "Disco Bar Spain", 
                    EmailPro = "francesco.bigi.87@gmail.com", 
                    Description = "Disco Bar Spain since 1990.",
                    LogoPath = "default-picture (8).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData { City = "Anverse", 
                        Country = "Belgium", Street = "Rue de l'Espagne", 
                        BoxNumber = "4", 
                        PostalCode = "1400"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (9).jpeg",
                    PhotoPath2 = "default-picture (10).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData { 
                    Type = BusinessTypeEnum.BAR.ToString(), 
                    Name = "Disco Bar Portugal", 
                    EmailPro = "francesco.bigi.87@gmail.com", 
                    Description = "Disco Bar Portugal since 1990.",
                    LogoPath = "default-picture (11).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue du Portugal", 
                        BoxNumber = "5", 
                        PostalCode = "1500"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (12).jpeg",
                    PhotoPath2 = "default-picture (13).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Concert Luxembourg", 
                    EmailPro = "francesco.bigi.87@gmail.com", 
                    Description = "Disco Concert Luxembourg since 1990.",
                    LogoPath = "default-picture (14).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Bruges",
                        Country = "Belgium",
                        Street = "Rue de Luxembourg",
                        BoxNumber = "6", PostalCode = "1600"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (15).jpeg",
                    PhotoPath2 = "default-picture (16).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CLUB.ToString(),
                    Name = "Disco Bar Holland", 
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco CLUB Holland since 1990.",
                    LogoPath = "default-picture (17).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Hollande",
                        BoxNumber = "7", 
                        PostalCode = "1700"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (18).jpeg",
                    PhotoPath2 = "default-picture (19).jpeg"
                },
                Disabled = true
            }); ;
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Concert Greece", 
                    EmailPro = "francesco.bigi.87@gmail.com", 
                    Description = "Disco Concert Greece since 1990.",
                    LogoPath = "default-picture (20).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Vilvoorde", 
                        Country = "Belgium", 
                        Street = "Rue de Grece", 
                        BoxNumber = "8", 
                        PostalCode = "1800"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (21).jpeg",
                    PhotoPath2 = "default-picture (22).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Concert Greece", 
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco Concert Sweden since 1990.",
                    LogoPath = "default-picture (23).jpeg"
                },
                BusinessInfo = new BusinessInfoData {
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", Street = "Rue de Swede", 
                        BoxNumber = "9", 
                        PostalCode = "1900" 

                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (24).jpeg",
                    PhotoPath2 = "default-picture (25).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.STUDENTCIRCLE.ToString(), 
                    Name = "Student circle Finland", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Student circle FInland since 1990.",
                    LogoPath = "default-picture (26).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427", 
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Finlande", 
                        BoxNumber = "10", 
                        PostalCode = "2000"

                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (27).jpeg",
                    PhotoPath2 = "default-picture (28).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Bar Poland", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Poland since 1990.",
                    LogoPath = "default-picture (29).jpeg"
                },
                BusinessInfo = new BusinessInfoData {
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse",
                        Country = "Belgium", 
                        Street = "Rue de Pologne", 
                        BoxNumber = "11", 
                        PostalCode = "2100"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (30).jpeg",
                    PhotoPath2 = "default-picture (31).jpeg"
                },
                Disabled = true
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Bar Croatie",
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco Croatie since 1990.",
                    LogoPath = "default-picture (32).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium",
                        Street = "Rue de Croatie", 
                        BoxNumber = "12", 
                        PostalCode = "2200"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (33).jpeg",
                    PhotoPath2 = "default-picture (34).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(),
                    Name = "Disco Bar Roumanie", 
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco Roumanie since 1990.",
                    LogoPath = "default-picture (35).jpeg"
                },
                BusinessInfo = new BusinessInfoData {
                    Phone = "+32493741427", 
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData
                    {
                        City = "Anverse",
                        Country = "Belgium",
                        Street = "Rue de Roumanie",
                        BoxNumber = "13", 
                        PostalCode = "2300"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (36).jpeg",
                    PhotoPath2 = "default-picture (37).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Bar Norvegia", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Norvegia since 1990.",
                    LogoPath = "default-picture (38).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { City = "Anverse",
                        Country = "Belgium", Street = "Rue de Norvege", 
                        BoxNumber = "14", 
                        PostalCode = "2400"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (39).jpeg",
                    PhotoPath2 = "default-picture (40).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(),
                    Name = "Disco Bar Ukraina", 
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco Ukraina since 1990.",
                    LogoPath = "default-picture (41).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Ukraine", 
                        BoxNumber = "15",
                        PostalCode = "2500"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (42).jpeg",
                    PhotoPath2 = "default-picture (43).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Bar Austria", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Austria since 1990.",
                    LogoPath = "default-picture (44).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de L'autriche",
                        BoxNumber = "16", 
                        PostalCode = "2600"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (45).jpeg",
                    PhotoPath2 = "default-picture (46).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(),
                    Name = "Disco Bar Czeque",
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Czeque since 1990.",
                    LogoPath = "default-picture (47).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com",
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium",
                        Street = "Rue de Republique Czeque", 
                        BoxNumber = "17", 
                        PostalCode = "2700"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (48).jpeg",
                    PhotoPath2 = "default-picture (49).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Concert Bar Cyprus", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Concert Cyprus since 1990.",
                    LogoPath = "default-picture (50).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427", 
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Cyprus", 
                        BoxNumber = "18", 
                        PostalCode = "2800"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (51).jpeg",
                    PhotoPath2 = "default-picture (52).jpeg"
                },
                Disabled = true
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.BAR.ToString(), 
                    Name = "Disco Bar Malta",
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Malta since 1990.",
                    LogoPath = "default-picture (53).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Malte", 
                        BoxNumber = "19", 
                        PostalCode = "2900"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (54).jpeg",
                    PhotoPath2 = "default-picture (55).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CLUB.ToString(), 
                    Name = "CLUB Lithuania", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Club Lithuania since 1990.",
                    LogoPath = "default-picture (56).jpeg"
                },
                BusinessInfo = new BusinessInfoData {
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Lithuanie",
                        BoxNumber = "20", 
                        PostalCode = "3000"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (57).jpeg",
                    PhotoPath2 = "default-picture (58).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.BAR.ToString(),
                    Name = "Disco Bar Estonia",
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Estonia since 1990.",
                    LogoPath = "default-picture (59).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Anverse",
                        Country = "Belgium", 
                        Street = "Rue de Estonie",
                        BoxNumber = "21", 
                        PostalCode = "3100"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (60).jpeg",
                    PhotoPath2 = "default-picture (61).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.BAR.ToString(),
                    Name = "Disco Bar Serbia",
                    EmailPro = "newbreaker@gmail.com",
                    Description = "Disco BAR Serbia since 1990.",
                    LogoPath = "default-picture (62).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { 
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Serbie", 
                        BoxNumber = "22", 
                        PostalCode = "3200"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (63).jpeg",
                    PhotoPath2 = "default-picture (64).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CONCERT.ToString(), 
                    Name = "Disco Bar Bielorussie", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Concert Bielorussie since 1990.",
                    LogoPath = "default-picture (1).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse",
                        Country = "Belgium", 
                        Street = "Rue de Bielorussie", 
                        BoxNumber = "23", 
                        PostalCode = "3300"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (2).jpeg",
                    PhotoPath2 = "default-picture (3).jpeg"
                }
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.STUDENTCIRCLE.ToString(),
                    Name = "Student circle Monaco", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Student circle Monaco since 1990.",
                    LogoPath = "default-picture (4).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData
                    {
                        City = "Anverse", 
                        Country = "Belgium", 
                        Street = "Rue de Monaco",
                        BoxNumber = "24",
                        PostalCode = "3400"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (5).jpeg",
                    PhotoPath2 = "default-picture (6).jpeg"
                },
                Disabled = true
            });
            context.BusinessDataModel.Add(new BusinessDataModel
            {
                WorkHours = new List<WorkHoursData> {
                    new WorkHoursData { Day = WorkHoursDayEnum.MONDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 30, MinuteTo = 59 },
                    new WorkHoursData { Day = WorkHoursDayEnum.TUESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.WEDNESDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.THURSDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 0, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.FRIDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = true, MinuteFrom = 0, MinuteTo = 0 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SATURDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 },
                    new WorkHoursData { Day = WorkHoursDayEnum.SUNDAY.ToString(), HourFrom = 8, HourTo = 17, Closed = false, MinuteFrom = 30, MinuteTo = 30 }
                },
                Identification = new IdentificationData
                {
                    Type = BusinessTypeEnum.CLUB.ToString(), 
                    Name = "Disco CLUB Moldavie", 
                    EmailPro = "newbreaker@gmail.com", 
                    Description = "Disco Moldavi since 1990.",
                    LogoPath = "default-picture (7).jpeg"
                },
                BusinessInfo = new BusinessInfoData { 
                    Phone = "+32493741427",  
                    EmailBusiness = "francesco.bigi.87@gmail.com", 
                    Address = new AddressData { City = "Anverse",
                        Country = "Belgium", Street = "Rue de Moldavie", 
                        BoxNumber = "25", PostalCode = "3500"
                    },
                    UrlSite = "http://users.telenet.be/biglekeux/Signature.html",
                    UrlFaceBook = "https://www.facebook.com/bigfra12",
                    UrlInstagram = "https://www.instagram.com/belgium",
                    UrlLinkedIn = "www.linkedin.com/in/bigif",
                    PhotoPath1 = "default-picture (8).jpeg",
                    PhotoPath2 = "default-picture (9).jpeg"
                }
            });
            context.SaveChangesAsync();
        }
    }
}
