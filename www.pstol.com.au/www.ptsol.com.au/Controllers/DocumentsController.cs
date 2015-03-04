using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Documents")]
    public class DocumentsController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [Route("Privacy-Policy")]
        public ActionResult Privacy_Policy()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Terms-of-Service")]
        public ActionResult Terms_of_Service()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}