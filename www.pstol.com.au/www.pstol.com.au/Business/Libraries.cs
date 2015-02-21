using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business
{
    public class Libraries:ILibraries
    {
        private ICaptchaLibrary captchas;

        public Libraries(IDataContext datacontext)
        {
            this.captchas = new CaptchaLibrary(datacontext);
        }

        public ICaptchaLibrary Captchas
        {
            get
            {
                return captchas;
            }
            set
            {
                captchas = value;
            }
        }
    }
}