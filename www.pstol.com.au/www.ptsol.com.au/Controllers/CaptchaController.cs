using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using log4net;
using www.ptsol.com.au;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;


namespace www.ptsol.com.au.Controllers
{
    [RoutePrefix("Captcha")]
    public class CaptchaController : Base
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CaptchaController));
        public CaptchaController(ILibraries library)
            : base(library)
        {
            log.Info("testing");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("SHOW-IMAGE/{guid}")]
        public FileContentResult GetImage(string guid)
        {
            try
            {
                string filename;
                string answer;

                if (HttpRuntime.Cache[guid] != null)
                {
                    Dictionary<string, string> cachecontent = HttpRuntime.Cache[guid] as Dictionary<string, string>;
                    filename = cachecontent["Filename"];
                    answer = cachecontent["Answer"];

                    HttpRuntime.Cache.Remove("guid");
                    HttpRuntime.Cache.Add(guid, cachecontent, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
                }
                else
                {
                    Captcha captcha = libraries.Captchas.GetRandom();
                    Dictionary<string, string> cachecontent = new Dictionary<string, string>();
                    cachecontent.Add("Filename", captcha.Filename);
                    cachecontent.Add("Answer", captcha.Answer);
                    filename = captcha.Filename;
                    answer = captcha.Answer;
                    HttpRuntime.Cache.Add(guid, cachecontent, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
                }

                byte[] data;
                string basedpath = Server.MapPath("/Images/Captcha/");
                string displayfilename = "SecurityQuestion.gif";
                FileStream stream = new FileStream(basedpath + @"\" + filename, FileMode.Open, FileAccess.Read);
                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                string mimeType = "image/gif";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + displayfilename);
                stream.Close();
                return File(data, mimeType);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return null;
        }
	}
}