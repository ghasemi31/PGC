using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class BranchOnlineOrderLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            string TimeNow = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);

            var list = from P in Context.Branches
                       where (P.AllowOnlineOrder && P.IsActive && P.AllowOnlineOrderTimeFrom.CompareTo(TimeNow)<=0 && P.AllowOnlineOrderTimeTo.CompareTo(TimeNow)>=0)
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
