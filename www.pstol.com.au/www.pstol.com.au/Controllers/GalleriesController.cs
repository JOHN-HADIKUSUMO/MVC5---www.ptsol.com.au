using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Galleries")]
    public class GalleriesController : Controller
    {
        public ActionResult Index()
        {
            //Response.RedirectPermanent("~/Index.html", true);
            return View();
        }

        [Route("Logo-Design")]
        public ActionResult Logo()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [Route("Web-Site-Development")]
        public ActionResult WebSite()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("Mobile-Application-Development")]
        public ActionResult Mobile()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}