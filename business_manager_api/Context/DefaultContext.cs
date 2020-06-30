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

        public DbSet<BusinessDataModel> BusinessData { get; set; }
        public DbSet<BusinessImageValidator> BusinessImage { get; set; }
        public DbSet<UserAccountModel> UserAccount { get; set; }

    }
}
