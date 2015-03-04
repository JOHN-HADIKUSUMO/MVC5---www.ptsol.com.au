using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business
{
    public class UserLibrary : DAL.UserService, IUserLibrary
    {
        public UserLibrary(IDataContext datacontext)
            : base(datacontext)
        {

        }

        public UserLibrary()
            : base()
        {

        }

    }
}