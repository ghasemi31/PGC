using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class SocialIconBusiness : BaseEntityManagementBusiness<SocialIcon, pgcEntities>
    {
        public SocialIconBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SocialIconPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.SocialIcons, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Url,
                    f.DispOrder,
                    f.Icon
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SocialIconPattern Pattern)
        {
            return Search_Where(Context.SocialIcons, Pattern).Count();
        }

        public IQueryable<SocialIcon> Search_Where(IQueryable<SocialIcon> list, SocialIconPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }

    }
}
