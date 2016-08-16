using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class GameCenterLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object City_ID)
        {
            if (City_ID == null)
                return null;

            var list = from P in Context.GameCenters.Where(c=>c.City_ID == (long)City_ID)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           Title= P.TItle
                       };

            return list;
        }

        
    }
}
