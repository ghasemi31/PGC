using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class ProductSoldChartBusiness
    {
        pgcEntities Context = new pgcEntities();

        public IQueryable<BranchTransaction> Search_Where(ProductSoldChartPattern Pattern)
        {
            IQueryable<BranchTransaction> transactionList = Context.BranchTransactions.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder || f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder || f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
            if (Pattern != null)
            {
                switch (Pattern.PersianDate.SearchMode)
                {
                    case DateRangePattern.SearchType.Between:
                        if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 &&
                                f.RegPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
                        break;
                    case DateRangePattern.SearchType.Greater:
                        if (Pattern.PersianDate.HasDate)
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0);
                        break;
                    case DateRangePattern.SearchType.Less:
                        if (Pattern.PersianDate.HasDate)
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0);
                        break;
                    case DateRangePattern.SearchType.Equal:
                        if (Pattern.PersianDate.HasDate)
                            transactionList = transactionList.Where(f => f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) == 0);
                        break;
                }
                if (Pattern.Branch_ID > 0)
                {
                    transactionList = transactionList.Where(f => f.Branch_ID == Pattern.Branch_ID);
                }

            }
            return transactionList;
        }

        public List<Sold> TransactionList(ProductSoldChartPattern Pattern)
        {
            var transactionList = Search_Where(Pattern);
            var transactionGroupByDate = transactionList.GroupBy(f => f.RegPersianDate);
            List<Sold> myList = new List<Sold>();
            if (Pattern.Product_ID < 1 && Pattern.ProductGroup_ID < 1)
            {
                foreach (var item in transactionGroupByDate)
                {
                    Sold sold = new Sold();
                    var branchOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder);
                    var sumBranchOrder = branchOrder.Sum(f => f.BranchDebt);
                    var branchReturnOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder);
                    var sumBranchReturnOrder = branchReturnOrder.Sum(f => f.BranchCredit);
                    var branchLackOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
                    var sumBranchLackOrder = branchLackOrder.Sum(f => f.BranchCredit);
                    var sum = sumBranchOrder - sumBranchReturnOrder - sumBranchLackOrder;
                    //sold.Date = item.FirstOrDefault().RegPersianDate;
                    sold.Date = kFrameWork.Util.DateUtil.GetEnglishDateTime(item.FirstOrDefault().RegPersianDate);
                    sold.ProductSold = sum;
                    myList.Add(sold);
                }
            }
            else
            {
                if (Pattern.Product_ID > 0)
                {
                    foreach (var item in transactionGroupByDate)
                    {
                        Sold sold = new Sold();
                        var branchOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder);
                        long sumBranchOrderDetail = 0, sumBranchReturnOrderDetail = 0, sumBranchLackOrderDetail = 0;
                        foreach (var OrderDetailItem in branchOrder)
                        {
                            var branchOrderDetail = Context.BranchOrderDetails.Where(f => f.BranchOrder_ID == OrderDetailItem.TransactionType_ID).Where(f => f.BranchOrderTitle_ID == Pattern.Product_ID);
                            if (branchOrderDetail.Count() > 0)
                            {
                                sumBranchOrderDetail += branchOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var branchReturnOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder);
                        foreach (var returnOrderItem in branchReturnOrder)
                        {
                            var branchReturnOrderDetail = Context.BranchReturnOrderDetails.Where(f => f.BranchReturnOrder_ID == returnOrderItem.TransactionType_ID).Where(f => f.BranchOrderTitle_ID == Pattern.Product_ID);
                            if (branchReturnOrderDetail.Count() > 0)
                            {
                                sumBranchReturnOrderDetail += branchReturnOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var branchLackOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
                        foreach (var LackOrderItem in branchLackOrder)
                        {
                            var branchLackOrderDetail = Context.BranchLackOrderDetails.Where(f => f.BranchLackOrder_ID == LackOrderItem.TransactionType_ID).Where(f => f.BranchOrderTitle_ID == Pattern.Product_ID);
                            if (branchLackOrderDetail.Count() > 0)
                            {
                                sumBranchLackOrderDetail += branchLackOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var sum = sumBranchOrderDetail - sumBranchReturnOrderDetail - sumBranchLackOrderDetail;
                        //sold.Date = item.FirstOrDefault().RegPersianDate;
                        sold.Date = kFrameWork.Util.DateUtil.GetEnglishDateTime(item.FirstOrDefault().RegPersianDate);
                        sold.ProductSold = sum;
                        myList.Add(sold);
                    }                   
                }
                if (Pattern.Product_ID < 1 && Pattern.ProductGroup_ID > 0)
                {
                    foreach (var item in transactionGroupByDate)
                    {
                        Sold sold = new Sold();
                        var branchOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchOrder);
                        long sumBranchOrderDetail = 0, sumBranchReturnOrderDetail = 0, sumBranchLackOrderDetail = 0;
                        foreach (var OrderDetailItem in branchOrder)
                        {
                            var branchOrderDetail = Context.BranchOrderDetails.Where(f => f.BranchOrder_ID == OrderDetailItem.TransactionType_ID).Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.ProductGroup_ID);
                            if (branchOrderDetail.Count() > 0)
                            {
                                sumBranchOrderDetail += branchOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var branchReturnOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchReturnOrder);
                        foreach (var returnOrderItem in branchReturnOrder)
                        {
                            var branchReturnOrderDetail = Context.BranchReturnOrderDetails.Where(f => f.BranchReturnOrder_ID == returnOrderItem.TransactionType_ID).Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.ProductGroup_ID);
                            if (branchReturnOrderDetail.Count() > 0)
                            {
                                sumBranchReturnOrderDetail += branchReturnOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var branchLackOrder = item.Where(f => f.TransactionType == (int)pgc.Model.Enums.BranchTransactionType.BranchLackOrder);
                        foreach (var LackOrderItem in branchLackOrder)
                        {
                            var branchLackOrderDetail = Context.BranchLackOrderDetails.Where(f => f.BranchLackOrder_ID == LackOrderItem.TransactionType_ID).Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.ProductGroup_ID);
                            if (branchLackOrderDetail.Count() > 0)
                            {
                                sumBranchLackOrderDetail += branchLackOrderDetail.FirstOrDefault().TotalPrice;
                            }
                        }
                        var sum = sumBranchOrderDetail - sumBranchReturnOrderDetail - sumBranchLackOrderDetail;
                        //sold.Date = item.FirstOrDefault().RegPersianDate;
                        sold.Date = kFrameWork.Util.DateUtil.GetEnglishDateTime(item.FirstOrDefault().RegPersianDate);
                        sold.ProductSold = sum;
                        myList.Add(sold);
                    }
                }
            }
            return myList;
        }

        public string RetriveBranchName(long id)
        {
            if (id > 0)
            {
                return Context.Branches.FirstOrDefault(f => f.ID == id).Title;
            }
            return "شعب";
        }

        public string RetriveBranchOrderTitleGroup(long id)
        {
            return Context.BranchOrderTitleGroups.FirstOrDefault(f => f.ID == id).Title;
        }
        public string RetriveProductTitle(long id)
        {
            if (id > 0)
            {
                return Context.BranchOrderTitles.FirstOrDefault(f => f.ID == id).Title;
            }
            return "کالا";
        }

        public class Sold
        {
            public DateTime Date { get; set; }
            //public string Date { get; set; }
            public long ProductSold { get; set; }
        }

    }
}
