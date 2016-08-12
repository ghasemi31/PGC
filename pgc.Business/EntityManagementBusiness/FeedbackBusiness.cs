using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class FeedbackBusiness: BaseEntityManagementBusiness<Feedback,pgcEntities>
    {
        public FeedbackBusiness()
        {
            Context = new pgcEntities();
        }
        public IQueryable Search_Select(int startRowIndex, int maximumRows, FeedbackPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Feedbacks, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.FullName,
                    f.Email,
                    f.Mobile,
                    f.Body
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(FeedbackPattern Pattern)
        {
            return Search_Where(Context.Feedbacks, Pattern).Count();
        }

        public IQueryable<Feedback> Search_Where(IQueryable<Feedback> list, FeedbackPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.FullName.Contains(Pattern.Title) ||
                    f.Email.Contains(Pattern.Title) || f.Body.Contains(Pattern.Title) || f.Mobile.Contains(Pattern.Title) || f.PersianDate.Contains(Pattern.Title));
            return list;
        }
    }
}
