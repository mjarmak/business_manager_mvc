using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace authentication_api.Context
{
    // IdentityDbContext contains all the user tables
    public class DefaultContext : IdentityDbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}
