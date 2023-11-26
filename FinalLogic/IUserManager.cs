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

        bool AddRole(string userId, string roleId, string editUser);

        bool RemoveRole(string userId, string roleId, string editUser);

        List<string> GetUsers();

        List<string> GetFacts();

    }
}
