using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.Business.Interfaces;

namespace www.ptsol.com.au.Business
{
    public class DataContext: IDataContext
    {
        private ptsolDBContext datacontext;

        public DataContext()
        {
            datacontext = new ptsolDBContext();
        }

        public ptsolDBContext GetEntity()
        {
            return datacontext;
        }
    }
}