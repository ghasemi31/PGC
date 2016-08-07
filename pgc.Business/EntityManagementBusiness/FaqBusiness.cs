using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class FaqBusiness:BaseEntityManagementBusiness<Faq,pgcEntities>
    {
        public FaqBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, FaqPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

          

            var Result = Search_Where(Context.Faqs, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Body,
                    f.Summery,
                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(FaqPattern Pattern)
        {
            return Search_Where(Context.Faqs, Pattern).Count();
        }

        public IQueryable<Faq> Search_Where(IQueryable<Faq> list, FaqPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title) || f.Summery.Contains(Pattern.Title));

            
            return list;
        }

       
    }
}