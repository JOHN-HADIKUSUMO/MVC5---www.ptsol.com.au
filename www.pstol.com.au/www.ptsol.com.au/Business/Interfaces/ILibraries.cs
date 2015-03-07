using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business.Interfaces
{
    public interface ILibraries
    {
        ptsolDBContext Context { get; }
        ICaptchaLibrary Captchas { get; set; }
        IUserLibrary Users { get; set; }
        ITestimonialLibrary Testimonials { get; set; }
    }
}
