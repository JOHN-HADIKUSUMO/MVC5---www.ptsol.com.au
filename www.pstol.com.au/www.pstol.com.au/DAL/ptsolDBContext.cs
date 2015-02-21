using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using www.ptsol.com.au.DAL.Models;

namespace www.ptsol.com.au.DAL
{
    public class ptsolDBContext : IdentityDbContext<ptsolUser>
    {
        public ptsolDBContext():base("DefaultConnection")
        {

        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }


        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.RoleId);
            }

        }


        public DbSet<Captcha> Captchas { get; set; }

        //public DbSet<ModelClass2> AnyName { get; set; }

        //public DbSet<ModelClass3> MoreClassesName { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}