using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www.ptsol.com.au.DAL.Interfaces
{
    public interface ICRUD<T0, T1, T2, T>
    {
        T0 Create(T t);
        T Read(T1 t);
        IQueryable<T> ReadAll();
        T2 Update(T t);
        T2 Delete(T1 t);
    }
}
