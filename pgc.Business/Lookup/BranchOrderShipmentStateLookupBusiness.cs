using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class BranchOrderShipmentStateLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.BranchOrderShipmentStates
                       orderby P.Title
                       select new
                       {
                           P.ID,
                           Title = P.Title
                       };

            return list;
        }
    }
}
