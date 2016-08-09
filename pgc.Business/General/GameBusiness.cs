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
    public class GameBusiness
    {
        pgcEntities db = new pgcEntities();

        //public IQueryable Game_List(object PageID)
        //{

        //    var Result = Game_Where(db.Gameertisements, ConvertorUtil.ToInt64(PageID))
        //        .OrderByDescending(a => a.DispOrder);

        //    return Result;


        //}

        //public int Game_Count(object PageID)
        //{
        //    return Game_Where(db.Gameertisements, ConvertorUtil.ToInt64(PageID)).Count();
        //}

        //public IQueryable<Gameertisement> Game_Where(IQueryable<Gameertisement> list,long PageID)
        //{
        //    string TimeNow = DateUtil.GetPersianDateShortString(DateTime.Now);
        //    return list.Where(a => a.ExpirePersianDate.CompareTo(TimeNow) > 0 && a.PanelPages.Any(p =>p.ID == PageID));
        //}

    }
}
