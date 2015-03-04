using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business
{
    public class Libraries:ILibraries
    {
        private ICaptchaLibrary captchas;
        private IUserLibrary users;

        public ptsolDBContext Context { get; private set; }
        public Libraries(IDataContext datacontext)
        {
            this.Context = datacontext.GetEntity();
            this.captchas = new CaptchaLibrary(datacontext);
            this.users = new UserLibrary(datacontext);
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

        public IUserLibrary Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }
    }
}