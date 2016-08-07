using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class RandomImageBusiness : BaseEntityManagementBusiness<MainSlider, pgcEntities>
    {
        public RandomImageBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, RandomImagePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.MainSliders, Pattern)
                 .Select(f => new
                 {
                     f.ID,
                     f.Title,
                     f.ImgPath,
                     f.DispOrder,
                     f.IsVisible
                 });

            return Result.OrderBy(r=>r.ID).Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(RandomImagePattern Pattern)
        {
            return Search_Where(Context.MainSliders, Pattern).Count();
        }

        public IQueryable<MainSlider> Search_Where(IQueryable<MainSlider> list, RandomImagePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;
            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(u => u.Title.Contains(Pattern.Title));

            return list;
        }
    }
}


