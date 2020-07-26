using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace business_manager_api.Context
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        public DbSet<BusinessDataModel> BusinessDataModel { get; set; }
        public DbSet<IdentificationData> Identification { get; set; }
        public DbSet<BusinessInfoData> BusinessInfo { get; set; }
        public DbSet<AddressData> Address { get; set; }
        public DbSet<UserAccountDataModel> UserAccount { get; set; }
        public DbSet<WorkHoursData> WorkHours { get; set; }
    }
}
