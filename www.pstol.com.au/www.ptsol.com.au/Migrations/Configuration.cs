namespace www.ptsol.com.au.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using www.ptsol.com.au.DAL;
    using www.ptsol.com.au.DAL.Interfaces;
    using www.ptsol.com.au.DAL.Models;
    using www.ptsol.com.au.Business;
    using www.ptsol.com.au.Business.Interfaces;

    internal sealed class Configuration : DbMigrationsConfiguration<ptsolDBContext>
    {
        private ILibraries library;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ptsolDBContext context)
        {
            library = new Libraries(new DataContext());

            UserManager<ptsolUser> manager = new UserManager<ptsolUser>(new UserStore<ptsolUser>(new ptsolDBContext()));

            RoleManager<IdentityRole> role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!role.RoleExists <IdentityRole>("Administrator"))
            {
                role.Create<IdentityRole>(new IdentityRole("Administrator"));

                if (manager.FindByName("Administrator") == null)
                {
                    string userid = library.Users.Create(new ptsolUser("administrator", "ptsol!@#$%^&*()_+", "Mr", "John", "Hadikusumo", "administrator@ptsol.com.au",true));

                    manager.AddToRole(userid, "Administrator");
                }
            }


            if (!role.RoleExists<IdentityRole>("Customer"))
            {
                role.Create<IdentityRole>(new IdentityRole("Customer"));
            }

            if (context.Captchas.Count() == 0)
            {
                context.Captchas.Add(new Captcha("1plus1.gif", "2"));
                context.Captchas.Add(new Captcha("1plus2.gif", "3"));
                context.Captchas.Add(new Captcha("1plus3.gif", "4"));
                context.Captchas.Add(new Captcha("1plus4.gif", "5"));
                context.Captchas.Add(new Captcha("1plus5.gif", "6"));
                context.Captchas.Add(new Captcha("2plus1.gif", "3"));
                context.Captchas.Add(new Captcha("2plus2.gif", "4"));
                context.Captchas.Add(new Captcha("2plus3.gif", "5"));
                context.Captchas.Add(new Captcha("2plus4.gif", "6"));
                context.Captchas.Add(new Captcha("2plus5.gif", "7"));
                context.Captchas.Add(new Captcha("3plus1.gif", "4"));
                context.Captchas.Add(new Captcha("3plus2.gif", "5"));
                context.Captchas.Add(new Captcha("3plus3.gif", "6"));
                context.Captchas.Add(new Captcha("3plus4.gif", "7"));
                context.Captchas.Add(new Captcha("3plus5.gif", "8"));
                context.Captchas.Add(new Captcha("4plus1.gif", "5"));
                context.Captchas.Add(new Captcha("4plus2.gif", "6"));
                context.Captchas.Add(new Captcha("4plus3.gif", "7"));
                context.Captchas.Add(new Captcha("4plus4.gif", "8"));
                context.Captchas.Add(new Captcha("4plus5.gif", "9"));
                context.Captchas.Add(new Captcha("4times1.gif", "4"));
                context.Captchas.Add(new Captcha("4times2.gif", "8"));
                context.Captchas.Add(new Captcha("4times3.gif", "12"));
                context.Captchas.Add(new Captcha("4times4.gif", "16"));
                context.Captchas.Add(new Captcha("4times5.gif", "20"));
                context.Captchas.Add(new Captcha("5plus1.gif", "6"));
                context.Captchas.Add(new Captcha("5plus2.gif", "7"));
                context.Captchas.Add(new Captcha("5plus3.gif", "8"));
                context.Captchas.Add(new Captcha("5plus4.gif", "9"));
                context.Captchas.Add(new Captcha("5plus5.gif", "10"));
                context.Captchas.Add(new Captcha("5times1.gif", "5"));
                context.Captchas.Add(new Captcha("5times2.gif", "10"));
                context.Captchas.Add(new Captcha("5times3.gif", "15"));
                context.Captchas.Add(new Captcha("5times4.gif", "20"));
                context.Captchas.Add(new Captcha("5times5.gif", "25"));
            }
        }
    }
}
