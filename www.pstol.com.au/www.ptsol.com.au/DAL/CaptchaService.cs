using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;


namespace www.ptsol.com.au.DAL
{
    public class CaptchaService : Base, ICaptchaService
    {
        public CaptchaService(IDataContext datacontext)
            : base(datacontext)
        {

        }
        public CaptchaService()
            : base()
        {

        }

        public int Create(Captcha model)
        {
            entity.Captchas.Add(model);
            entity.SaveChanges();
            return model.CaptchaId;
        }

        public Captcha Read(int id)
        {
            return entity.Captchas.Where(w => w.CaptchaId == id && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
        }

        public IQueryable<Captcha> ReadAll()
        {
            return entity.Captchas.Where(w => w.IsDeleted == null || w.IsDeleted == false).AsQueryable<Captcha>();
        }

        public bool Update(Captcha model)
        {
            Captcha tempCaptcha = entity.Captchas.Where(w => w.CaptchaId == model.CaptchaId && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
            if (tempCaptcha == null) return false;
            tempCaptcha.Filename = model.Filename;
            tempCaptcha.Answer = model.Answer;
            entity.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Captcha tempCaptcha = entity.Captchas.Where(w => w.CaptchaId == id && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
            if (tempCaptcha == null) return false;
            tempCaptcha.IsDeleted = true;
            tempCaptcha.DeletedDate = DateTime.Now;
            entity.SaveChanges();
            return true;
        }
    }
}