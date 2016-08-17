using kFrameWork.Business;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class AdvertismentBusiness: BaseEntityManagementBusiness<Advertisment, pgcEntities>
    {
        public AdvertismentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AdvertismentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Advertisments, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Adv_Url,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(AdvertismentPattern Pattern)
        {
            return Search_Where(Context.Advertisments, Pattern).Count();
        }

        public IQueryable<Advertisment> Search_Where(IQueryable<Advertisment> list, AdvertismentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title)) ;

       

            return list;
        }



       
    }
}