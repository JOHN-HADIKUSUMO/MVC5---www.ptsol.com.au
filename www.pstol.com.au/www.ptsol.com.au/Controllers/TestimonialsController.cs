using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;
using www.ptsol.com.au.Models;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("TESTIMONIAL")]
    public class TestimonialsController : Controller
    {
        private ILibraries libraries;
        public TestimonialsController(ILibraries libraries)
        {
            this.libraries = libraries;
        }

        public ActionResult TopTwo()
        {
            List<Testimonial> model = libraries.Testimonials.GetTopTwo();

            return View(model);
        }
    }
}