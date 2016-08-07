using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class BranchesOrderReportBusiness
    {
        pgcEntities db = new pgcEntities();
        public IQueryable<BranchOrderDetail> RetriveOrder(BranchesOrderReportPattern pattern)
        {
            var orderList = Search(db.BranchOrderDetails, pattern);
            return orderList;
        }

        public List<string> HeaderTable(BranchesOrderReportPattern pattern)
        {
            var list = Search(db.BranchOrderDetails, pattern);
            List<string> categories =list.Select(m => m.BranchOrderTitle.Title).Distinct().ToList();
            return categories;
        }


        //public List<Branch> RowBranch()
        //{
        //    var list = db.BranchOrderDetails.Where(m => m.BranchOrder.RegPersianDate == "1392/05/30");
        //    List<Branch> branch = list.Select(b=>b.BranchOrder.Branch).Distinct().ToList();
        //    return branch;
        //}

        public IQueryable<BranchOrderDetail> Search(IQueryable<BranchOrderDetail> list, BranchesOrderReportPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null) 
            {
                string date=kFrameWork.Util.DateUtil.GetPersianDateShortString(DateTime.Now);
                return list.Where(f => f.BranchOrder.RegPersianDate == date );
            }

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.BranchOrderTitle_Title.Contains(Pattern.Title));

            if (Pattern.Branch_ID>0)
            {
                list = list.Where(f => f.BranchOrder.Branch_ID == Pattern.Branch_ID);
            }

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => f.BranchOrder.RegPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0
                            && f.BranchOrder.RegPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.BranchOrder.RegPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.BranchOrder.RegPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.BranchOrder.RegPersianDate.CompareTo(Pattern.PersianDate.Date) == 0);
                    break;
            }

            //Search by Status
            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.BranchOrder.Status == (int)Pattern.Status);

            return list;
        }
    }
}
