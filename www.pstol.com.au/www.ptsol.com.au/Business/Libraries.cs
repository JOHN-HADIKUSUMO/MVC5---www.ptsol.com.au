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
        private ITestimonialLibrary testimonials;

        public ptsolDBContext Context { get; private set; }
        public Libraries(IDataContext datacontext)
        {
            this.Context = datacontext.GetEntity();
            this.captchas = new CaptchaLibrary(datacontext);
            this.users = new UserLibrary(datacontext);
            this.testimonials = new TestimonialLibrary(datacontext);
        }

        public ITestimonialLibrary Testimonials
        {
            get
            {
                return testimonials;
            }
            set
            {
                testimonials = value;
            }
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