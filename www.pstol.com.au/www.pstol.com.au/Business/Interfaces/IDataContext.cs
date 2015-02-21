using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www.ptsol.com.au.DAL;

namespace www.ptsol.com.au.Business.Interfaces
{
    public interface IDataContext
    {
        ptsolDBContext GetEntity();
    }
}
