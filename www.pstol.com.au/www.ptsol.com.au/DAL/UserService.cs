using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;


namespace www.ptsol.com.au.DAL
{
    public class UserService : Base, IUserService
    {
        public UserService(IDataContext datacontext)
            : base(datacontext)
        {

        }
        public UserService()
            : base()
        {

        }

        public string Create(ptsolUser model)
        {
            entity.Users.Add(model);
            entity.SaveChanges();
            return model.Id;
        }

        public ptsolUser Read(string id)
        {
            return entity.Users.Where(w => w.Id == id).FirstOrDefault();
        }

        public ptsolUser ReadByUsername(string username)
        {
            return entity.Users.Where(w => w.UserName.Trim().ToUpper() == username.Trim().ToUpper()).FirstOrDefault();
        }

        public IQueryable<ptsolUser> ReadAll()
        {
            return entity.Users.AsQueryable<ptsolUser>();
        }

        public bool Update(ptsolUser model)
        {
            ptsolUser tempUser = entity.Users.Where(w => w.Id == model.Id).FirstOrDefault();
            if (tempUser == null) return false;
            tempUser.Title = model.Title;
            tempUser.Firstname = model.Firstname;
            tempUser.Lastname = model.Lastname;
            tempUser.Email = model.Email;
            entity.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            ptsolUser tempUser = entity.Users.Where(w => w.Id == id).FirstOrDefault();
            if (tempUser == null) return false;
            entity.SaveChanges();
            return true;
        }

        public bool CheckEmail(string email)
        {
            return entity.Users.Where(w => w.Email == email).Any();
        }

        public bool CheckUsername(string username)
        {
            return entity.Users.Where(w => w.UserName == username).Any();
        }

        public string ResetPassword(string username)
        {
            PasswordHasher hasher = new PasswordHasher();
            string newpassword = Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 8);
            string newsecuritystamp = Guid.NewGuid().ToString();
            ptsolUser user = entity.Users.Where(w => w.UserName == username).FirstOrDefault();
            user.PasswordHash = hasher.HashPassword(newpassword);
            user.SecurityStamp = newsecuritystamp;
            entity.SaveChanges();
            return newsecuritystamp;
        }


        public bool CheckConfirmation(string username)
        {
            return entity.Users.Where(w => w.UserName == username).Select(s=>s.IsConfirmed).FirstOrDefault();
        }
    }
}