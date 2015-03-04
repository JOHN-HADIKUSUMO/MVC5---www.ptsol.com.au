using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using www.ptsol.com.au.Models;
using www.ptsol.com.au.Models.REST;

namespace www.ptsol.com.au.DAL
{
    public class ptsolUser:IdentityUser
    {
        public ptsolUser()
        {

        }
        public ptsolUser(inRegistrationForm form)
        {
            PasswordHasher hasher = new PasswordHasher();
            this.Id = Guid.NewGuid().ToString();
            this.UserName = form.username;
            this.PasswordHash = hasher.HashPassword(form.password);
            this.Title = form.title;
            this.Firstname = form.firstname;
            this.Lastname = form.lastname;
            this.Email = form.email;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }
        public ptsolUser(string username,string password,string title,string firstname,string lastname,string email)
        {
            PasswordHasher hasher = new PasswordHasher();
            this.Id = Guid.NewGuid().ToString();
            this.UserName = username;
            this.PasswordHash = hasher.HashPassword(password);
            this.Title = title;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        public ptsolUser(string username, string password, string title, string firstname, string lastname, string email,bool isconfirmed)
        {
            PasswordHasher hasher = new PasswordHasher();
            this.Id = Guid.NewGuid().ToString();
            this.UserName = username;
            this.PasswordHash = hasher.HashPassword(password);
            this.Title = title;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.SecurityStamp = Guid.NewGuid().ToString();
            this.IsConfirmed = isconfirmed;
        }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }

    }
}