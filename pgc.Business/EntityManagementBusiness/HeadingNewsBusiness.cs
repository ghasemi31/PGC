using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class HeadingNewsBusiness:BaseEntityManagementBusiness<Heading,pgcEntities>
    {
        public HeadingNewsBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, HeadingNewsPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Headings, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigateUrl,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(HeadingNewsPattern Pattern)
        {
            return Search_Where(Context.Headings, Pattern).Count();
        }

        public IQueryable<Heading> Search_Where(IQueryable<Heading> list, HeadingNewsPattern Pattern)
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