using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Resources;
using System.Globalization;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;
using www.ptsol.com.au.Models.REST;

namespace www.ptsol.com.au.Utilities.SendEmail
{
    public class SendActivationLink:Base
    {
        private string username;
        private string token;
        private ILibraries libraries;
        public string Username
        {
            get
            {
                return this.username;
            }

            set{
                this.username = value;   
            }
        }

        public string Token
        {
            get
            {
                return this.token;
            }

            set
            {
                this.token = value;
            }
        }

        public ILibraries Libraries
        {
            get
            {
                return this.libraries;
            }

            set
            {
                this.libraries = value;
            }
        }

        public SendActivationLink():base()
        {
            mailPriority = MailPriority.High;
            isHTML = true;
        }

        public SendActivationLink(ILibraries libraries, string username, string token)
            : base()
        {
            this.libraries = libraries;
            this.username = username;
            this.token = token;
            mailPriority = MailPriority.High;
            isHTML = true;
        }

        public override void Send()
        {
            base.Send();

            /* Start Getting User info */
            ptsolUser user = libraries.Users.ReadByUsername(this.username);

            StringBuilder toDisplayName = new StringBuilder();
            if(user.Title.Trim()!="")
            {
                toDisplayName.Append(user.Title.Trim() + " ");
            }

            toDisplayName.Append(user.Firstname + " " + user.Lastname);

            StringBuilder toEmailAddress = new StringBuilder(user.Email);
            /* End Getting User info */

            /* Start Getting Templates from Resources */
            ResourceManager resourceManager = new ResourceManager("www.ptsol.com.au.Resources.Email", typeof(www.ptsol.com.au.Resources.Email).Assembly);

            StringBuilder subject = new StringBuilder(resourceManager.GetString("AccountActivationSubject"));
            StringBuilder body = new StringBuilder(resourceManager.GetString("AccountActivationBody"));
            body = body.Replace("[!--baseurl--!]", ConfigurationManager.AppSettings["httpBaseURL"].ToString() + ":" + ConfigurationManager.AppSettings["httpBasePort"].ToString());
            body = body.Replace("[!--url--!]", ConfigurationManager.AppSettings["httpBaseURL"].ToString() + ":" + ConfigurationManager.AppSettings["httpBasePort"].ToString() + "/account/activation/" + this.username + "/" + this.token);
            body = body.Replace("[!--facebook--!]",ConfigurationManager.AppSettings["httpFacebook"].ToString());
            body = body.Replace("[!--twitter--!]",ConfigurationManager.AppSettings["httpTwitter"].ToString());
            body = body.Replace("[!--instagram--!]",ConfigurationManager.AppSettings["httpInstagram"].ToString());
            body = body.Replace("[!--username--!]", this.username);

            /* End Getting Templates from Resources */

            MailAddress to = new MailAddress(toEmailAddress.ToString(), toDisplayName.ToString());
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["teamEmail"].ToString(), ConfigurationManager.AppSettings["teamDisplayName"].ToString());
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = isHTML;
            message.Priority = mailPriority;
            message.Subject = subject.ToString();
            message.Body = body.ToString();
            smtpclient.Send(message);
        }
    }
}