using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.ptsol.com.au.Models.REST
{
    public class inChangePasswordForm
    {
        public string username{get;set;}
        public string stamp { get; set; }
        public string password {get;set;}
    }
}