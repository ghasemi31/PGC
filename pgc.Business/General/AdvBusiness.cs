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
    public class AdvBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Adv_List(object PageID)
        {

            var Result = Adv_Where(db.Advertisements, ConvertorUtil.ToInt64(PageID))
                .OrderByDescending(a => a.DispOrder);

            return Result;


        }

        public int Adv_Count(object PageID)
        {
            return Adv_Where(db.Advertisements, ConvertorUtil.ToInt64(PageID)).Count();
        }

        public IQueryable<Advertisement> Adv_Where(IQueryable<Advertisement> list,long PageID)
        {
            string TimeNow = DateUtil.GetPersianDateShortString(DateTime.Now);
            return list.Where(a => a.ExpirePersianDate.CompareTo(TimeNow) > 0 && a.PanelPages.Any(p =>p.ID == PageID));
        }

    }
}
