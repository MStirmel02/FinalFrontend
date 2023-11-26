using FinalDataObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FinalDataLayer
{
    public interface IUserAccess
    {
        bool ValidateUser(UserModel user);

        List<string> GetUserRole(UserModel user);
        
        bool CreateUser(UserModel user);

        bool AddUserRole(string userId, string roleId, string editUser);

        bool RemoveUserRole(string userId, string roleId, string editUser);

        RestResponse GetUsers();
        
    }
}
