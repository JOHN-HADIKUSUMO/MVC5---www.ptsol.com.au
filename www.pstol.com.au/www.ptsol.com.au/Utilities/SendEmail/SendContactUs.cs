using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using www.ptsol.com.au.Models.REST;

namespace www.ptsol.com.au.Utilities.SendEmail
{
    public class SendContactUs:Base
    {
        private inContactUsForm model;
        public inContactUsForm Model
        {
            get
            {
                return this.model;
            }

            set{
                this.model = value;   
            }
        }
        public SendContactUs():base()
        {
            mailPriority = MailPriority.High;
            isHTML = false;
        }

        public SendContactUs(inContactUsForm model)
            : base()
        {
            this.model = model;
            mailPriority = MailPriority.High;
            isHTML = false;
        }

        public override void Send()
        {
            base.Send();

            MailAddress to = new MailAddress(ConfigurationManager.AppSettings["teamEmail"].ToString(), ConfigurationManager.AppSettings["teamDisplayName"].ToString());
            MailAddress from = new MailAddress(model.email, " From Contact Us - " + model.name);
            MailMessage message = new MailMessage(from, to);
            if (model.sendemail)
            {
                message.Bcc.Add(from);
            }
            message.IsBodyHtml = isHTML;
            message.Priority = mailPriority;
            message.Subject = model.subject;
            message.Body = model.message;
            smtpclient.Send(message);
        }
    }
}