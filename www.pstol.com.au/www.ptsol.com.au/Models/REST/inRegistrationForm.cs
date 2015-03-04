using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.ptsol.com.au.Models.REST
{
    public class inRegistrationForm
    {
        public string username{get;set;}
        public string title { get; set; }
        public string firstname {get;set;}
        public string lastname {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public string conpassword {get;set;}
        public string answer {get;set;}
        public bool agree {get;set;}
        public string guid { get; set; }
    }
}