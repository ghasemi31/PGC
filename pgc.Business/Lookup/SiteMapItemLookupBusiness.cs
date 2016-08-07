using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class SiteMapItemLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object Cat_ID)
        {
            if (Cat_ID == null)
                return null;

            var list = from P in Context.SiteMapItems.Where(i=>i.SiteMapCat_ID == (long)Cat_ID)
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
