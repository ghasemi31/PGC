using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class AdvPageLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList()
        {
            var list = from C in Context.PanelPages
                       where C.AdvSupport == true
                       orderby C.ID descending
                       select new
                       {
                           C.ID,
                           C.Title
                       };

            return list;
        }

        
    }
}
