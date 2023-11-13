using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FinalDataObjects;
using RestSharp;

namespace FinalDataLayer
{
    public class UserAccess
    {
        RestClient client = new RestClient();

        
        public bool ValidateUser(UserModel user)
        {
            RestRequest request = new RestRequest("https://localhost:44333/UserAuth");
            request.AddJsonBody(user);
            

            var response = client.Post(request);

            if (response.Content == "true" )
            {
                return true;
            } else
            {
                return false;
            }
        }

        public List<string> GetUserRole(UserModel user)
        {
            RestRequest request = new RestRequest("https://localhost:44333/UserAuth/Roles/");
            request.AddQueryParameter("userId", user.UserId);

            var response = client.Get(request);
            List<string> result = response.Content.Split('/').ToList();
            return new List<string>();
        }


    }
}
