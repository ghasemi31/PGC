using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class ReportBusiness:BaseEntityManagementBusiness<Report,pgcEntities>
    {
        public ReportBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, ReportPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Reports, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Summary
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(ReportPattern Pattern)
        {
            return Search_Where(Context.Reports, Pattern).Count();
        }

        public IQueryable<Report> Search_Where(IQueryable<Report> list, ReportPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Summary.Contains(Pattern.Title) || f.Body.Contains(Pattern.Title));

            
          
            return list;
        }

        //public OperationResult UpdateIsDone(long Report_ID)
        //{
        //    Report Data = this.Retrieve(Report_ID);
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