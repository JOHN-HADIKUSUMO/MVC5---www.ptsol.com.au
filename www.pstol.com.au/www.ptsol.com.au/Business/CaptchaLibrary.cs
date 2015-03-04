using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business
{
    public class CaptchaLibrary : DAL.CaptchaService, ICaptchaLibrary
    {
        public CaptchaLibrary(IDataContext datacontext)
            : base(datacontext)
        {

        }

        public CaptchaLibrary()
            : base()
        {

        }

        public Captcha GetRandom()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 35);
            return Read(id);
        }
    }
}