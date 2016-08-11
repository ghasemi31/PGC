using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class GameLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.Games
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
