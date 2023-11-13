using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogic
{
    public interface IUserManager
    {

        string HashSha256(string source);

        UserModel LoginUser(UserModel user);

    }
}
