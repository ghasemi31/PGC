using kFrameWork.Business;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.Lookup
{
    public class SideMenuCatLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.SideMenuCats
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
