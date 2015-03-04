using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace www.ptsol.com.au.Utilities
{
    public static class RegEx
    {
        public static bool isValidEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
        }

        public static bool isValidTelephone(string telephone)
        {
            string strRegex = @"\+?\(?\d{2,4}\)?[\d\s-]{3,}";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(telephone))
                return (true);
            else
                return (false);
        }

        
    }
}