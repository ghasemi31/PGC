using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class MainMenuBusiness: BaseEntityManagementBusiness<MainMenu, pgcEntities>
    {
        public MainMenuBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, MainMenuPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

           var Result = Search_Where(Context.MainMenus, Pattern)
                .OrderByDescending(f => f.DisplayOrder)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigationUrl,
                    f.DisplayOrder,
                    f.IsBlank,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(MainMenuPattern Pattern)
        {
            return Search_Where(Context.MainMenus, Pattern).Count();
        }

        public IQueryable<MainMenu> Search_Where(IQueryable<MainMenu> list, MainMenuPattern Pattern)
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


