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
    public class DynPageBusiness
    {
        pgcEntities db = new pgcEntities();

        //public IQueryable DynPage_List(int startRowIndex, int maximumRows)
        //{
        //    if (startRowIndex == 0 && maximumRows == 0)
        //        return null;

        //    var Result = DynPage_Where(db.DynPages)
        //        .OrderByDescending(f => f.ID)
        //        .Select(f => new
        //        {
        //            f.ID,
        //            f.Title,
        //            f.Summary 
                    
        //        });

        //     return Result.Skip(startRowIndex).Take(maximumRows);

        //}
        //public int DynPage_Count()
        //{
        //    return DynPage_Where(db.DynPage).Count();
        //}
        //public IQueryable<DynPage> DynPage_Where(IQueryable<DynPage> list)
        //{
        //    return list;
        //}



        public DynPage RetriveDynPage(string urlKey)
        {
            return db.DynPages.Where(f => f.UrlKey==urlKey).SingleOrDefault();
        }

    }
}
