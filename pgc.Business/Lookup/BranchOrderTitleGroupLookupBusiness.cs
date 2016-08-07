using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class BranchOrderTitleGroupLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.BranchOrderTitleGroups
                       orderby P.DisplayOrder
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        
    }
}
