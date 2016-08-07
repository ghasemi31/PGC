using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class BranchLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.Branches
                       where P.IsActive == true
                       orderby P.DispOrder
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        
    }
}
