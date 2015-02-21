using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Controllers
{
    public class Base : Controller
    {
        protected ILibraries libraries;
        public Base(ILibraries libraries)
        {
            this.libraries = libraries;
        }
	}
}