using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

            try
            {
                var response = client.Get(request);
                objects = JsonConvert.DeserializeObject<List<ObjectModel>>(response.Content);
                return objects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ObjectModel> GetRequestList()
        {
            List<ObjectModel> objects = new List<ObjectModel>();


            RestRequest request = new RestRequest("https://localhost:44333/Object/Requests");

            try
            {
                var response = client.Get(request);
                objects = JsonConvert.DeserializeObject<List<ObjectModel>>(response.Content);
                return objects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FullObjectModel GetObjectById(string id)
        {
            FullObjectModel objectModel = new FullObjectModel();

            RestRequest request = new RestRequest("https://localhost:44333/Object/id");
            request.AddQueryParameter("id", id);

            
            try
            {
                var response = client.Get(request);
                objectModel = JsonConvert.DeserializeObject<FullObjectModel>(response.Content);
                return objectModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditObjectById(FullObjectModel obj, string userId, string action)
        {
            RestRequest request = new RestRequest("https://localhost:44333/Object/");
            
            request.AddBody(obj);
            request.AddQueryParameter("userId", userId);
            request.AddQueryParameter("action", action);

            try
            {
                var response = client.Patch(request);

                if (response.Content == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        public List<string> GetObjectTypes()
        {
            RestRequest request = new RestRequest("https://localhost:44333/Object/ObjectTypes");

            List<string> typeList = new List<string>();
            try
            {
                var response = client.Get(request);

                typeList = JsonConvert.DeserializeObject<List<string>>(response.Content);
                return typeList;

            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
