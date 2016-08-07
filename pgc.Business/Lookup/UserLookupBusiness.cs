using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class UserLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList()
        {
            var list = from P in Context.Users
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           Title = P.Fname + " " + P.Lname
                       };

            return list;
        }

        public IQueryable GetLookupList(long user_ID)
        {
            var list = from P in Context.Users.Where(f=>f.AccessLevel.Role==(int)Role.Admin)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           Title = P.Fname + " " + P.Lname
                       };

            return list;
        }
    }
}
