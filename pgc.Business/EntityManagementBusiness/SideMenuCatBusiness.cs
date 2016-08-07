using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.EntityManagementBusiness
{
    public class SideMenuCatBusiness : BaseEntityManagementBusiness<SideMenuCat, pgcEntities>
    {
        public SideMenuCatBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SideMenuCatPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

           var Result = Search_Where(Context.SideMenuCats, Pattern)
                .OrderByDescending(f => f.DisplayOrder)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigationUrl,
                    f.DisplayOrder,
                    f.IsBlank,
                    f.IsVisible
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SideMenuCatPattern Pattern)
        {
            return Search_Where(Context.SideMenuCats, Pattern).Count();
        }

        public IQueryable<SideMenuCat> Search_Where(IQueryable<SideMenuCat> list, SideMenuCatPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;
            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(u => u.Title.Contains(Pattern.Title));

           return list;
        }
    }
}

