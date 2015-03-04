using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace www.ptsol.com.au.Utilities.SendEmail
{
    public abstract class AbstractBase
    {
        protected string smtpHost;
        protected int smtpPort;
        protected NetworkCredential credential;
        protected SmtpClient smtpclient;

        public AbstractBase()
        {
            smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
            smtpPort = int.Parse(ConfigurationManager.AppSettings["smtpPort"].ToString());
            credential = new NetworkCredential(ConfigurationManager.AppSettings["smtpHostUsername"].ToString(),ConfigurationManager.AppSettings["smtpHostPassword"].ToString());
            smtpclient = new SmtpClient(smtpHost, smtpPort);
            smtpclient.Credentials = credential;
        }
        public abstract void Send();
    }
}