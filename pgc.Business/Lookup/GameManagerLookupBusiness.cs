using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class GameManagerLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.Users.Where(p=>p.AccessLevel.Role==(int)Role.Agent)
                       orderby P.ID 
                       select new
                       {
                           P.ID,
                          Title= P.Email
                       };

            return list;
        }

        
    }
}
