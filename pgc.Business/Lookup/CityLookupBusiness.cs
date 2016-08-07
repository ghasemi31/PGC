using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class CityLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object Province_ID)
        {
            if (Province_ID == null)
                return null;

            var list = from P in Context.Cities.Where(c=>c.Province_ID == (long)Province_ID)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        
    }
}
