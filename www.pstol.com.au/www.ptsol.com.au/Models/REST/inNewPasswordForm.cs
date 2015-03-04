using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.ptsol.com.au.Models.REST
{
    public class inNewPasswordForm
    {
        public string username{get;set;}
        public string token { get; set; }
        public string password {get;set;}
        public string conpassword {get;set;}
        public string answer {get;set;}
        public string guid { get; set; }
    }
}