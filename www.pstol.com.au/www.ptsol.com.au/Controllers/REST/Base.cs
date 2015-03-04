using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Configuration;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Controllers.REST
{
    public class Base : ApiController
    {
        protected ILibraries libraries;
        public Base(ILibraries libraries)
        {
            this.libraries = libraries;
        }
	}
}