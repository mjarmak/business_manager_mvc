using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace business_manager_api
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<BusinessDataModel> BusinessDataModel { get; set; }
        public DbSet<BusinessImageModel> BusinessImage { get; set; }
        public DbSet<IdentificationData> IdentificationData { get; set; }
        public DbSet<BusinessInfo> BusinessInfo { get; set; }
        public DbSet<UserAccountModel> UserAccount { get; set; }
        public DbSet<LogoModel> Logo { get; set; }

    }
}
