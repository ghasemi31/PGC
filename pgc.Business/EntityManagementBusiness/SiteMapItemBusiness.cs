using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class SiteMapItemBusiness:BaseEntityManagementBusiness<SiteMapItem,pgcEntities>
    {
        public SiteMapItemBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SiteMapItemPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.SiteMapItems, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigateUrl,
                    f.IsVisible,
                    SiteMapCat =f.SiteMapCat.Title
                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SiteMapItemPattern Pattern)
        {
            return Search_Where(Context.SiteMapItems, Pattern).Count();
        }

        public IQueryable<SiteMapItem> Search_Where(IQueryable<SiteMapItem> list, SiteMapItemPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (Pattern.SiteMapCat_ID > 0)
                list = list.Where(f => f.SiteMapCat_ID == Pattern.SiteMapCat_ID);

            return list;
        }
       
    }
}