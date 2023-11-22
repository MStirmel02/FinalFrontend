using FinalDataObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalDataLayer
{
    public class CommentAccess : ICommentAccess
    {
        RestClient _client;

        public RestResponse DeactivateComment(int commentId)
        {
            RestRequest request = new RestRequest("https://localhost:44333/Comment/id");

            request.AddQueryParameter("commentId", commentId);

            try
            {
                var response = _client.Delete(request);
                return response;
          
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public RestResponse EditComment(CommentModel comment, string oldDescription)
        { 
            RestRequest request = new RestRequest("https://localhost:44333/Comment");

            request.AddBody(comment);
            request.AddHeader("oldDescription", oldDescription);

            try
            {
                var response = _client.Patch(request);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RestResponse GetCommentsByObjectId(string ObjectId)
        {
            RestRequest request = new RestRequest("https://localhost:44333/Comment");

            request.AddQueryParameter("objectId", ObjectId);

            try
            {
                var response = _client.Get(request);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RestResponse PostComment(CommentModel comment)
        {
            RestRequest request = new RestRequest("https://localhost:44333/Comment");

            request.AddBody(comment);

            try
            {
                var response = _client.Post(request);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
