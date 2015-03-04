using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.ptsol.com.au.Models;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Contact-Us")]
    public class ContactUsController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.GUID = Guid.NewGuid().ToString();
            return View();
        }
    }
}