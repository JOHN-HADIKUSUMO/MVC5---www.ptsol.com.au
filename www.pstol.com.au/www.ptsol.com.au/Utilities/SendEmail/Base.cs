using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace www.ptsol.com.au.Utilities.SendEmail
{
    public class Base:AbstractBase
    {
        protected MailPriority mailPriority;
        protected bool isHTML;
        public Base():base()
        {
            mailPriority = MailPriority.Normal;
            isHTML = true;
        }

        public override void Send()
        {
            
        }
    }
}