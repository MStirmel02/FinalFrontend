using FinalDataLayer;
using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinalLogic
{
    public class UserManager : IUserManager
    {
        IUserAccess _userAccess;
        public UserManager()
        {
            _userAccess = new UserAccess();
        }
        public UserManager(IUserAccess userAccess) 
        {
            _userAccess = userAccess;
        }

        public string HashSha256(string source)
        {
            string hashValue = "";

            byte[] data;
            using (SHA256 sha256 = SHA256.Create())
            {
                data = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            hashValue = s.ToString();
            return hashValue;
        }

        public UserModel LoginUser(UserModel user)
        {
            try
            {
                if (_userAccess.ValidateUser(user))
                {
                    user.Roles = _userAccess.GetUserRole(user);
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw new ApplicationException();
                
            }
            
        }

        public bool CreateUser(UserModel user)
        {
            return _userAccess.CreateUser(user);
        }

        public bool AddRole(string userId, string roleId, string editUser)
        {
            return _userAccess.AddUserRole(userId, roleId, editUser);
        }

        public bool RemoveRole(string userId, string roleId, string editUser)
        {
            return _userAccess.RemoveUserRole(userId, roleId, editUser);
        }

        public List<string> GetUsers()
        {
            try
            {
                var response = JsonSerializer.Deserialize<List<string>>(_userAccess.GetUsers().Content);
                return response;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public List<string> GetFacts()
        {
            try
            {
                var response = JsonSerializer.Deserialize<List<string>>(_userAccess.GetFacts().Content);
                return response;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
