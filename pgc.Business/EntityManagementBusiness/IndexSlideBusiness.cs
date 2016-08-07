using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class IndexSlideBusiness:BaseEntityManagementBusiness<IndexSlide,pgcEntities>
    {
        public IndexSlideBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, IndexSlidePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.IndexSlides, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigateUrl,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(IndexSlidePattern Pattern)
        {
            return Search_Where(Context.IndexSlides, Pattern).Count();
        }

        public IQueryable<IndexSlide> Search_Where(IQueryable<IndexSlide> list, IndexSlidePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }

        //public OperationResult UpdateIsDone(long Heading_ID)
        //{
        //    Heading Data = this.Retrieve(Heading_ID);
        //    OperationResult Res = new OperationResult();
        //    if (Data != null)
        //    {
        //        Data.IsDone = !Data.IsDone;
        //        Res=this.Update(Data);
        //    }

        //    return Res;
        //}
    }
}