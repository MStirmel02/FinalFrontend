using FinalDataLayer;
using FinalDataObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalLogic
{
    public class CommentManager :ICommentManager
    {
        ICommentAccess _commentAccess = new CommentAccess();
        public CommentManager()
        {

        }
        public CommentManager(ICommentAccess commentAccess) 
        {
            _commentAccess = commentAccess;
        }

        public bool DeactivateComment(int commentId)
        {

            try
            {
                RestResponse response = _commentAccess.DeactivateComment(commentId);


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

        public bool EditComment(CommentModel comment, string oldDescription)
        {
            try
            {
                RestResponse response = _commentAccess.EditComment(comment, oldDescription);
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

        public List<CommentModel> GetCommentsByObjectId(string ObjectId)
        {
            try
            {
                RestResponse response = _commentAccess.GetCommentsByObjectId(ObjectId);
                if (response.Content == null)
                {
                    return new List<CommentModel>();
                }
                List<CommentModel> comments = JsonConvert.DeserializeObject<List<CommentModel>>(response.Content);
                return comments;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool PostComment(CommentModel comment)
        {
            try
            {
                RestResponse response = _commentAccess.PostComment(comment);
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
    }
}
