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
    public class HelpBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Help_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Help_Where(db.Helps)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    
                });

             return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Help_Count()
        {
            return Help_Where(db.Helps).Count();
        }
        public IQueryable<Help> Help_Where(IQueryable<Help> list)
        {
            return list;
        }



        public Help RetriveHelp(long ID)
        {
            return db.Helps.Where(f => f.ID == ID).SingleOrDefault();
        }

    }
}
