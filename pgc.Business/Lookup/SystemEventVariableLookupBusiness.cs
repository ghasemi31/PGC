using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class SystemEventVariableLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object SystemEvent_ID)
        {
            if (SystemEvent_ID == null)
                return null;

            var list = from P in Context.SystemEventVariables.Where(c => c.SystemEvent_ID == (long)SystemEvent_ID)
                       orderby P.ID descending
                       select new
                       {
                           ID=P.Alias,
                           Title = P.Title
                           //Title = P.Title+"  ("+P.Alias+")"
                       };

            return list;
        }

        
    }
}
