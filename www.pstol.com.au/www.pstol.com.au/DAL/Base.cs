using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.DAL
{
    public class Base
    {
        protected ptsolDBContext entity = null;

        public Base()
        {
            entity = new ptsolDBContext();
        }
        public Base(IDataContext datacontext)
        {
            this.entity = datacontext.GetEntity();
        }
    }
}