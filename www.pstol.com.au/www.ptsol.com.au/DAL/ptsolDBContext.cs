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

        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Captcha> Captchas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ptsolUser>().HasMany<Testimonial>(m => m.u);
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }
    }
}