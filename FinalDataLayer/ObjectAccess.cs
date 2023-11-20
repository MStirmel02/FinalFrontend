using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalDataObjects;
using Newtonsoft.Json;
using RestSharp;

namespace FinalDataLayer
{
    public class ObjectAccess : IObjectAccess
    {
        RestClient client = new RestClient();


        public List<ObjectModel> GetObjectList()
        {
            List<ObjectModel> objects = new List<ObjectModel>();


            RestRequest request = new RestRequest("https://localhost:44333/Object");

            var response = client.Get(request);

            try
            {
                objects = JsonConvert.DeserializeObject<List<ObjectModel>>(response.Content);
                return objects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
