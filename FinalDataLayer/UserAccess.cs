using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using FinalDataObjects;
using Newtonsoft.Json;
using RestSharp;

namespace FinalDataLayer
{
    public class UserAccess : IUserAccess
    {
        RestClient client = new RestClient();

        
        public bool ValidateUser(UserModel user)
        {
            RestRequest request = new RestRequest("https://localhost:44333/User");
            request.AddJsonBody(user);
            

            var response = client.Post(request);

            if (response.Content == "true" )
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public List<string> GetUserRole(UserModel user)
        {
            RestRequest request = new RestRequest("https://localhost:44333/User/Roles/");
            request.AddQueryParameter("userId", user.UserId);

            var response = client.Get(request).Content;

            try
            {
                List<string> roles = JsonConvert.DeserializeObject<List<string>>(response);
                return roles;
            }
            catch (Exception)
            {

                throw new HttpException();
            }
        }

        public bool CreateUser(UserModel user)
        {
            RestRequest request = new RestRequest("https://localhost:44333/User");
            request.AddBody(user);

            var response = client.Post(request);

            if (response.Content == "true")
            {
                return true;
            } 
            else
            {
                return false;
            }
         }

        public bool AddUserRole(string userId, string roleId, string editUser)
        {
            RestRequest request = new RestRequest("https://localhost:44333/User/Roles/Add");
            request.AddHeader("editUser", editUser);
            request.AddHeader("roleId", roleId);
            request.AddHeader("userId", userId);

            var response = client.Post(request);
            
            if (response.Content == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveUserRole(string userId, string roleId, string editUser)
        {
            RestRequest request = new RestRequest("https://localhost:44333/User/Roles/Delete");
            request.AddHeader("editUser", editUser);
            request.AddHeader("roleId", roleId);
            request.AddHeader("userId", userId);

            var response = client.Delete(request);

            if (response.Content == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public RestResponse GetUsers()
        {
            RestRequest request = new RestRequest("https://localhost:44333/User");

            var response = client.Get(request);

            try
            {
                return response;
            }
            catch (Exception)
            {
                return new RestResponse();
            }
        }
        
        public RestResponse GetFacts()
        {
            RestRequest request = new RestRequest("https://localhost:44333/User/Facts");

            var response = client.Get(request);

            try
            {
                return response;
            }
            catch (Exception)
            {
                return new RestResponse();
            }
        }
    }
}
