using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class PollChoiseBusiness:BaseEntityManagementBusiness<PollChoise,pgcEntities>
    {
        public PollChoiseBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, PollChoisePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.PollChoises, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Description,
                    Poll = f.Poll.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(PollChoisePattern Pattern)
        {
            return Search_Where(Context.PollChoises, Pattern).Count();
        }

        public IQueryable<PollChoise> Search_Where(IQueryable<PollChoise> list, PollChoisePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Description.Contains(Pattern.Title));

            if (Pattern.Poll_ID > 0)
                list = list.Where(f => f.Poll_ID == Pattern.Poll_ID);

            return list;
        }

    }
}