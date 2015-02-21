using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using www.ptsol.com.au.Controllers;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.pstol.com.au.Tests.Controllers
{
    [TestClass]
    public class DataContextControllerTest
    {
        [TestMethod]
        public void Init_Test()
        {
            ptsolDBContext dbcontext = new ptsolDBContext();

            Assert.IsNotNull(dbcontext);
        }

        [TestMethod]
        public void Captchas_Test()
        {
            ptsolDBContext dbcontext = new ptsolDBContext();


            Assert.IsNotNull(dbcontext.Captchas);
        }

    }
}
