using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class CommentBusiness : BaseEntityManagementBusiness<Comment,pgcEntities>
    {
        public CommentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, CommentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Comments, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.SenderName,
                    f.SenderEmail,
                    f.IsDisplay,
                    f.Body
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(CommentPattern Pattern)
        {
            return Search_Where(Context.Comments, Pattern).Count();
        }

        public IQueryable<Comment> Search_Where(IQueryable<Comment> list, CommentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.SenderName.Contains(Pattern.Title) ||
                    f.SenderEmail.Contains(Pattern.Title) || f.Body.Contains(Pattern.Title));
                return list;
        }

    }
}