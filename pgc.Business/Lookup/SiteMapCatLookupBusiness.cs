using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class SiteMapCatLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.SiteMapCats
                       orderby P.Title 
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        
    }
}
