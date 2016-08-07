using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class HeadingBusiness:BaseEntityManagementBusiness<Heading,pgcEntities>
    {
        public HeadingBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, HeadingPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Headings, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    //f.Description
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(HeadingPattern Pattern)
        {
            return Search_Where(Context.Headings, Pattern).Count();
        }

        public IQueryable<Heading> Search_Where(IQueryable<Heading> list, HeadingPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }


    }
}