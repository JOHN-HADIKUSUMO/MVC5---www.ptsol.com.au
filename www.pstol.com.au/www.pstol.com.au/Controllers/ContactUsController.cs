using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.pstol.com.au.Models;

namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Contact-Us")]
    public class ContactUsController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            ContactUsModel model = new ContactUsModel();
            return View(model);
        }

        [Route("")]
        [HttpPost]
        public ActionResult Index(ContactUsModel  model)
        {

            return View(model);
        }
    }
}