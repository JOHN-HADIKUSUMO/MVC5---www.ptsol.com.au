using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www.ptsol.com.au.DAL.Models;

namespace www.ptsol.com.au.DAL.Interfaces
{
    public interface IUserService : ICRUD<string, string, bool, ptsolUser>
    {
        bool CheckConfirmation(string username);
        bool CheckEmail(string email);
        bool CheckUsername(string username);
        string ResetPassword(string username);
        ptsolUser ReadByUsername(string username);
    }
}
