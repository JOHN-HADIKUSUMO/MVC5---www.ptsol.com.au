using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("About-Us")]
    public class AboutUsController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}