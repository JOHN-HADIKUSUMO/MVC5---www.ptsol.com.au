using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.ptsol.com.au.Models.REST
{
    public class inContactUsForm
    {
        public string name {get;set;}
        public string email {get;set;}
        public string mobile {get;set;}
        public string subject {get;set;}
        public string message {get;set;}
        public string answer {get;set;}
        public bool sendemail {get;set;}
        public string guid { get; set; }
    }
}