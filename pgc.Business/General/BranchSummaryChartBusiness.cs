using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class BranchSummaryChartBusiness
    {
        pgcEntities Context = new pgcEntities();



        public IQueryable<BranchCredit> Search_Where(BranchSummaryChartPattern Pattern)
        {
            List<BranchCredit> result = new List<BranchCredit>();

            foreach (var item in Context.Branches.Where(f => f.IsActive).OrderBy(f => f.ID))
            {
                BranchCredit credit = new BranchCredit();

                credit.ID = item.ID;
                credit.MinimumCredit = item.MinimumCredit;
                credit.Title = item.Title;
                credit.DisplayOrder = item.DispOrder;

                long currentCredit = 0;
                var creditList = Context.BranchTransactions.Where(f => f.Branch_ID == credit.ID);
                if (Pattern!=null)
                {
                    switch (Pattern.PersianDate.SearchMode)
                    {
                        case DateRangePattern.SearchType.Between:
                            if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                                creditList = creditList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0
                    && f.RegPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
                            break;
                        case DateRangePattern.SearchType.Greater:
                            if (Pattern.PersianDate.HasDate)
                                creditList = creditList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0);
                            break;
                        case DateRangePattern.SearchType.Less:
                            if (Pattern.PersianDate.HasDate)
                                creditList = creditList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0);
                            break;
                        case DateRangePattern.SearchType.Equal:
                            if (Pattern.PersianDate.HasDate)
                                creditList = creditList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) == 0);
                            break;
                    }
                    
                }

                if (creditList.Count() > 0)
                    currentCredit = creditList.Sum(g => g.BranchCredit - g.BranchDebt);

                if (currentCredit < 0)
                {
                    credit.CurrentCredit = 0;
                    credit.CurrentDebt = currentCredit;
                    //credit.Status = BranchCreditStatus.InDebt;
                }
                else
                {
                    credit.CurrentCredit = currentCredit;
                    credit.CurrentDebt = 0;
                    //credit.Status = BranchCreditStatus.InCredit;
                }

                result.Add(credit);
            }

            var ResultList = result.AsQueryable();

           return ResultList;
        }



        public class BranchCredit
        {
            public BranchCredit()
            {
                Title = "";
            }
            public long ID { get; set; }
            public string Title { get; set; }
            public long DisplayOrder { get; set; }
            public long MinimumCredit { get; set; }
            public long CurrentCredit { get; set; }
            public long CurrentDebt { get; set; }
            //public BranchCreditStatus Status { get; set; }
        }
    }
}
