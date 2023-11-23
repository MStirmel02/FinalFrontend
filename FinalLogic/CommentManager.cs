using FinalDataLayer;
using FinalDataObjects;
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
        ICommentAccess _commentAccess;
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
                return JsonSerializer.Deserialize<List<CommentModel>>(response.Content);
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
