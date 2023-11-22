using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogic
{
    public interface ICommentManager
    {
        List<CommentModel> GetCommentsByObjectId(string ObjectId);

        bool PostComment(CommentModel comment);

        bool DeactivateComment(int commentId);

        bool EditComment(CommentModel comment, string oldDescription);
    }
}
