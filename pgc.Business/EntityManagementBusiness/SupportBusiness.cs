using System;
using System.Linq;
using System.Web;
using System.Xml;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using System.Collections.Generic;
using System.Globalization;
using kFrameWork.UI;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class SupportBusiness : BaseEntityManagementBusiness<Support, pgcEntities>
    {
        public SupportBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SupportPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Supports, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Link
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SupportPattern Pattern)
        {
            return Search_Where(Context.Supports, Pattern).Count();
        }

        public IQueryable<Support> Search_Where(IQueryable<Support> list, SupportPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title)) ;

       

            return list;
        }



        public IEnumerable<Support> getAllSupport()
        {
            return Context.Supports;
        }
    }
}