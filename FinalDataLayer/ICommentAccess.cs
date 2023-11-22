using FinalDataObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDataLayer
{
    public interface ICommentAccess
    {
        RestResponse GetCommentsByObjectId(string ObjectId);

        RestResponse PostComment(CommentModel comment);

        RestResponse DeactivateComment(int commentId);

        RestResponse EditComment(CommentModel comment, string oldDescription);

    }
}
