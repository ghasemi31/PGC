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
    public class ReportBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Report_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Report_Where(db.Reports)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Summary,
                    f.ThumbImageUrl

                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Report_Count()
        {
            return Report_Where(db.Reports).Count();
        }
        public IQueryable<Report> Report_Where(IQueryable<Report> list)
        {
            return list;
        }



        public Report RetriveReport(long id)
        {
            return db.Reports.Where(f => f.ID == id).SingleOrDefault();
        }

    }
}
