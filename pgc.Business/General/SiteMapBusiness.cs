using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;

namespace pgc.Business.General
{
    public class SiteMapBusiness
    {
        pgcEntities db = new pgcEntities();

        public List<SiteMapCat> GetSiteMapCat()
        {
            return db.SiteMapCats.OrderBy(c => c.DispOrder).ToList();
        }

        public List<SiteMapItem> GetSiteMapItem(object Cat_ID)
        {
            return db.SiteMapItems.Where(i=> i.SiteMapCat_ID== (long)Cat_ID)
                                  .OrderBy(i => i.DispOrder).ToList();
        }

        
    }
}
