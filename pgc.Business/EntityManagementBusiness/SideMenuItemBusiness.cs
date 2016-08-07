using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
   public class SideMenuItemBusiness:BaseEntityManagementBusiness<SideMenuItem,pgcEntities>
    {
       public SideMenuItemBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SideMenuItemPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.SideMenuItems, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigateUrl,
                    f.IsVisible,
                    SideMenuCat =f.SideMenuCat.Title
                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SideMenuItemPattern Pattern)
        {
            return Search_Where(Context.SideMenuItems, Pattern).Count();
        }

        public IQueryable<SideMenuItem> Search_Where(IQueryable<SideMenuItem> list, SideMenuItemPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (Pattern.SideMenuCat_ID > 0)
                list = list.Where(f => f.SideMenuCat_ID == Pattern.SideMenuCat_ID);

            return list;
        }
       
    }
}