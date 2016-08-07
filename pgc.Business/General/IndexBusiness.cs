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
    public class IndexBusiness
    {
        pgcEntities db = new pgcEntities();

        public List<Heading> GetIndexHeading()
        {
            return db.Headings.OrderBy(h => h.DispOrder).ToList();
        }

        public List<IndexSlide> GetIndexSlide()
        {
            return db.IndexSlides.OrderBy(i => i.DispOrder).ToList();
        }
        
    }
}
