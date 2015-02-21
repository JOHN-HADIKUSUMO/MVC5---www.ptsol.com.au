using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Jobs")]
    public class JobsController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {

            return View();
        }
    }
}