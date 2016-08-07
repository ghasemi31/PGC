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
    public class BranchesOrderReportBusiness : BaseEntityManagementBusiness<BranchOrderDetail, pgcEntities>
    {
        public BranchesOrderReportBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchesOrderReportPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.BranchOrderDetails, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Quantity,
                    f.BranchOrderTitle_Title,
                    f.BranchOrder
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchesOrderReportPattern Pattern)
        {
            return Search_Where(Context.BranchOrderDetails, Pattern).Count();
        }

        public IQueryable<BranchOrderDetail> Search_Where(IQueryable<BranchOrderDetail> list, BranchesOrderReportPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            //if (!string.IsNullOrEmpty(Pattern.Title))
            //    list = list.Where(f => f.Title.Contains(Pattern.Title) ||
            //        f.Description.Contains(Pattern.Title));


            //switch (Pattern.StartDate.SearchMode)
            //{
            //    case DateRangePattern.SearchType.Between:
            //        if (Pattern.StartDate.HasFromDate && Pattern.StartDate.HasToDate)
            //            list = list.Where(f => f.StartPersianDate.CompareTo(Pattern.StartDate.FromDate) >= 0
            //                && f.StartPersianDate.CompareTo(Pattern.StartDate.ToDate) <= 0);
            //        break;
            //    case DateRangePattern.SearchType.Greater:
            //        if (Pattern.StartDate.HasDate)
            //            list = list.Where(f => f.StartPersianDate.CompareTo(Pattern.StartDate.Date) >= 0);
            //        break;
            //    case DateRangePattern.SearchType.Less:
            //        if (Pattern.StartDate.HasDate)
            //            list = list.Where(f => f.StartPersianDate.CompareTo(Pattern.StartDate.Date) <= 0);
            //        break;
            //    case DateRangePattern.SearchType.Equal:
            //        if (Pattern.StartDate.HasDate)
            //            list = list.Where(f => f.StartPersianDate.CompareTo(Pattern.StartDate.Date) == 0);
            //        break;
            //}

            //switch (Pattern.EndDate.SearchMode)
            //{
            //    case DateRangePattern.SearchType.Between:
            //        if (Pattern.EndDate.HasFromDate && Pattern.EndDate.HasToDate)
            //            list = list.Where(f => f.EndPersianDate.CompareTo(Pattern.EndDate.FromDate) >= 0
            //                && f.EndPersianDate.CompareTo(Pattern.EndDate.ToDate) <= 0);
            //        break;
            //    case DateRangePattern.SearchType.Greater:
            //        if (Pattern.EndDate.HasDate)
            //            list = list.Where(f => f.EndPersianDate.CompareTo(Pattern.EndDate.Date) >= 0);
            //        break;
            //    case DateRangePattern.SearchType.Less:
            //        if (Pattern.EndDate.HasDate)
            //            list = list.Where(f => f.EndPersianDate.CompareTo(Pattern.EndDate.Date) <= 0);
            //        break;
            //    case DateRangePattern.SearchType.Equal:
            //        if (Pattern.EndDate.HasDate)
            //            list = list.Where(f => f.EndPersianDate.CompareTo(Pattern.EndDate.Date) == 0);
            //        break;
            //}

            return list;
        }

        //public OperationResult UpdateIsDone(long Sample_ID)
        //{
        //    Sample Data = this.Retrieve(Sample_ID);
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