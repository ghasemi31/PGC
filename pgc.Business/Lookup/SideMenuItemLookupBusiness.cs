using kFrameWork.Business;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.Lookup
{
    public class SideMenuItemLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object Cat_ID)
        {
            if (Cat_ID == null)
                return null;

            var list = from P in Context.SideMenuItems.Where(i=>i.SideMenuCat_ID== (long)Cat_ID)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        
    }
}