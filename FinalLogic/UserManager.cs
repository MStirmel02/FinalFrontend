using FinalDataLayer;
using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogic
{
    public class UserManager : IUserManager
    {
        UserAccess _userAccess = new UserAccess();
        public UserManager() 
        {
            _userAccess = new UserAccess();
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
    }
}
