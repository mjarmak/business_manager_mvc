using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    public class DefaultContext : DbContext
    {

        public DefaultContext() : base("Business_DEV")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DefaultContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<DefaultContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DefaultContext>());
        }

        public DbSet<BusinessData> BusinessData { get; set; }
        public DbSet<BusinessImage> BusinessImage { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }

    }
}
