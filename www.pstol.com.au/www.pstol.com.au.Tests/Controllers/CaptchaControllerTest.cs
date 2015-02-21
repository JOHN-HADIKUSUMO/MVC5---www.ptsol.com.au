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
    public class CaptchaControllerTest
    {
        [TestMethod]
        public void Read_Positive_Test()
        {
            ILibraries libraries = new Libraries(new DataContext());
            Captcha captcha = libraries.Captchas.Read(1);
            Assert.IsNotNull(captcha);
        }

        [TestMethod]
        public void Read_Negative_Test()
        {
            ILibraries libraries = new Libraries(new DataContext());
            Captcha captcha = libraries.Captchas.Read(100); /* captchaid 100 doesn't exist on database table */
            Assert.IsNotNull(captcha);
        }

        [TestMethod]
        public void ReadAll_Test()
        {
            ILibraries libraries = new Libraries(new DataContext());
            IQueryable<Captcha> captchas = libraries.Captchas.ReadAll();
            Assert.IsTrue(captchas.Any());
        }

    }
}
