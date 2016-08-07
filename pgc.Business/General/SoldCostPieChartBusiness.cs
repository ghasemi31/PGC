using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class SoldCostPieChartBusiness
    {
        pgcEntities Context = new pgcEntities();

        public IQueryable<BranchTransaction> Search_Where(DateRangePattern Pattern)
        {
            IQueryable<BranchTransaction> transactionList = 
                Context.BranchTransactions.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder ||
                    f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder ||
                    f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
            if (Pattern != null)
            {
                switch (Pattern.SearchMode)
                {
                    case DateRangePattern.SearchType.Between:                        
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.FromDate) >= 0 &&
                                f.RegPersianDate.CompareTo(Pattern.ToDate) <= 0);
                        break;
                    case DateRangePattern.SearchType.Greater:
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.Date) >= 0);
                        break;
                    case DateRangePattern.SearchType.Less:
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.Date) <= 0);
                        break;
                    case DateRangePattern.SearchType.Equal:
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.Date) == 0);
                        break;
                }
            }
            return transactionList;
        }


        public List<BranchSold> TransactionList(DateRangePattern Pattern)
        {
            var transactionList = Search_Where(Pattern);
            var transactionGroupByBranch = transactionList.GroupBy(f => f.Branch_ID);
            List<BranchSold> myList = new List<BranchSold>();
            foreach (var item in transactionGroupByBranch)
            {
                BranchSold branchSold = new BranchSold();
                var branchOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder);
                var sumBranchOrder = branchOrder.Sum(f => f.BranchDebt);
                var branchReturnOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder);
                var sumBranchReturnOrder = branchReturnOrder.Sum(f => f.BranchCredit);
                var branchLackOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
                var sumBranchLackOrder = branchLackOrder.Sum(f => f.BranchCredit);
                var sum = sumBranchOrder - sumBranchReturnOrder - sumBranchLackOrder;
                branchSold.BranchName = item.FirstOrDefault().Branch.Title;
                branchSold.Sold = sum;
                myList.Add(branchSold);
            }
            return myList;
        }
    }
    public class BranchSold
    {
        public string BranchName { get; set; }
        public long Sold { get; set; }
    }
}
