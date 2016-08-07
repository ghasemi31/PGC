using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class BranchOrderTitleLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.BranchOrderTitles
                       orderby P.DisplayOrder
                       where P.Status == (int)BranchOrderTitleStatus.Enabled
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        public IQueryable GetLookupList(object Group_ID)
        {
            if (Group_ID == null)
                return null;

            var list = from P in Context.BranchOrderTitles.Where(c => c.BranchOrderTitleGroup_ID == (long)Group_ID)
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
