using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using pgc.Business.Core;
using kFrameWork.Util;
using kFrameWork.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace pgc.Business
{
    public class BranchSoldBusiness : BaseEntityManagementBusiness<BranchTransaction, pgcEntities>
    {
        public BranchSoldBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchSoldPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Pattern)
               .OrderBy(f => f.DisplayOrder);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public long Search_SelectTotalAmount(BranchSoldPattern Pattern)
        {
            //var Result = Search_Where(Pattern);

            //if (Result.Count() > 0)
            //    return Result.Sum(f => f.Amount);

            var list = SearchDuringBranchTransactions(Pattern);

            if (Pattern.Type == BranchSoldType.Rejected)
            {
                var temp = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder);

                //if (Pattern.OrderTitle_ID < 1)
                //{
                    return (temp.Count() > 0) ? temp.Sum(f => f.BranchCredit) : 0;
                //}
                //else
                //{
                //    var ReturnIDs = temp.Select(f => f.TransactionType_ID);
                //    var _temp = Context.BranchReturnOrders.Where(f => ReturnIDs.Contains(f.ID) && f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //    return (_temp.Count() > 0) ? _temp.Sum(f => f.TotalPrice) : 0;
                //}
            }
            else if (Pattern.Type == BranchSoldType.Sold)
            {
                //if (Pattern.OrderTitle_ID < 1)
                //{
                    var orders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder);
                    var returnorders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder);
                    var lackorders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder);

                    long result = 0;
                    if (orders.Count() > 0)
                        result += orders.Sum(f => f.BranchDebt);

                    if (returnorders.Count() > 0)
                        result -= returnorders.Sum(f => f.BranchCredit);

                    if (lackorders.Count() > 0)
                        result -= lackorders.Sum(f => f.BranchCredit);

                    return result;
                //}
                //else
                //{
                //    var orders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder).Select(f => f.TransactionType_ID);
                //    var returnorders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder).Select(f => f.TransactionType_ID);
                //    var lackorders = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder).Select(f => f.TransactionType_ID);

                //    long result = 0;
                //    if (orders.Count() > 0)
                //    {
                //        var _temp = Context.BranchOrders.Where(f => orders.Contains(f.ID) && f.BranchOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //        if (_temp.Count() > 0)
                //            result += _temp.Sum(f => f.TotalPrice);
                //    }

                //    if (returnorders.Count() > 0)
                //    {
                //        var _temp = Context.BranchReturnOrders.Where(f => returnorders.Contains(f.ID) && f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //        if (_temp.Count() > 0)
                //            result -= _temp.Sum(f => f.TotalPrice);
                //    }

                //    if (lackorders.Count() > 0)
                //    {
                //        var _temp = Context.BranchLackOrders.Where(f => lackorders.Contains(f.ID) && f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //        if (_temp.Count() > 0)
                //            result -= _temp.Sum(f => f.TotalPrice);
                //    }
                //    return result;
                //}
            }

            return 0;
        }

        public int Search_Count(BranchSoldPattern Pattern)
        {
            //return Search_Where(Pattern).Count();
            var temp = SearchDuringBranchTransactions(Pattern);
            var counts = temp.Select(f => f.Branch_ID).ToList();
            return counts.Distinct().Count();
        }

        public IQueryable<BranchSold> Search_Where(BranchSoldPattern Pattern)
        {
            if (Pattern == null)
                return new List<BranchSold>().AsQueryable();

            return CreateBranchSoldList(SearchDuringBranchTransactions(Pattern), Pattern);
        }

        public IQueryable<BranchTransaction> SearchDuringBranchTransactions(BranchSoldPattern Pattern)
        {
            if (Pattern == null)
                return new List<BranchTransaction>().AsQueryable();

            List<BranchTransaction> list = new List<BranchTransaction>();



            switch (Pattern.Type)
            {
                case BranchSoldType.Sold:
                    list = Context.BranchTransactions.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder || f.TransactionType == (int)BranchTransactionType.BranchReturnOrder || f.TransactionType == (int)BranchTransactionType.BranchLackOrder).ToList();
                    break;
                case BranchSoldType.Rejected:
                    list = Context.BranchTransactions.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder).ToList();
                    break;
                default:
                    break;
            }



            switch (Pattern.SoldPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.SoldPersianDate.HasFromDate && Pattern.SoldPersianDate.HasToDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.RegPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0)).ToList();
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.SoldPersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0)).ToList();
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.SoldPersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0)).ToList();
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.SoldPersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0)).ToList();
                    break;
            }



            switch (Pattern.Price.Type)
            {
                case RangeType.Between:
                    if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
                        list = list.Where(f => (f.BranchCredit >= Pattern.Price.FirstNumber && f.BranchCredit <= Pattern.Price.SecondNumber) ||
                                                (f.BranchDebt >= Pattern.Price.FirstNumber && f.BranchDebt <= Pattern.Price.SecondNumber)).ToList();
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit >= Pattern.Price.FirstNumber || f.BranchDebt >= Pattern.Price.FirstNumber).ToList();
                    break;
                case RangeType.LessThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit <= Pattern.Price.FirstNumber || f.BranchDebt <= Pattern.Price.FirstNumber).ToList();
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit == Pattern.Price.FirstNumber || f.BranchDebt == Pattern.Price.FirstNumber).ToList();
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }



            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID).ToList();


            //////////////////////////////////
            {
            }
            //////////////////////////////////

            //if (Pattern.OrderTitle_ID > 0)
            //{



            //    //Create List Of BranchORDER IDs

            //    List<long> orderIDs = new List<long>();
            //    var OrderTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder);

            //    if (OrderTempList.Count() > 0)
            //        orderIDs = OrderTempList.Select(f => f.TransactionType_ID).ToList();



            //    List<long> lackIDs = new List<long>();
            //    var LackTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder);

            //    if (LackTempList.Count() > 0)
            //        lackIDs = LackTempList.Select(f => f.TransactionType_ID).ToList();



            //    List<long> returnIDs = new List<long>();
            //    var ReturnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder);

            //    if (ReturnTempList.Count() > 0)
            //        returnIDs = ReturnTempList.Select(f => f.TransactionType_ID).ToList();



            //    //Check For OrderTitle_ID in BranchORDER

            //    if (Pattern.Type == BranchSoldType.Sold)
            //    {
            //        if (orderIDs.Count() > 0)
            //        {
            //            var orderTempList = Context.BranchOrders.Where(f => orderIDs.Contains(f.ID) && f.BranchOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            //            if (orderTempList.Count() > 0)
            //            {
            //                orderIDs = orderTempList.Select(f => f.ID).ToList();
            //                list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchOrder && orderIDs.Contains(f.TransactionType_ID)) ||
            //                                        (f.TransactionType != (int)BranchTransactionType.BranchOrder)).ToList();
            //            }
            //            else
            //                list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchOrder).ToList();
            //        }



            //        if (lackIDs.Count() > 0)
            //        {
            //            var lackTempList = Context.BranchLackOrders.Where(f => lackIDs.Contains(f.ID) && f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            //            if (lackTempList.Count() > 0)
            //            {
            //                lackIDs = lackTempList.Select(f => f.ID).ToList();
            //                list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchLackOrder && lackIDs.Contains(f.TransactionType_ID)) ||
            //                                        (f.TransactionType != (int)BranchTransactionType.BranchLackOrder)).ToList();
            //            }
            //            else
            //                list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchLackOrder).ToList();
            //        }

            //    }


            //    //var returnTempList = Context.BranchReturnOrders.Where(f => returnIDs.Contains(f.ID));
            //    var returnTempList = Context.BranchReturnOrders.Where(f => returnIDs.Contains(f.ID) && f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            //    if (returnTempList.Count() > 0)
            //    {
            //        returnIDs = returnTempList.Select(f => f.ID).ToList();
            //        list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && returnIDs.Contains(f.TransactionType_ID)) ||
            //                                (f.TransactionType != (int)BranchTransactionType.BranchReturnOrder)).ToList();
            //    }
            //    else
            //        list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchReturnOrder).ToList();

            //}

            //{
            //    if (Pattern.GroupTitle_ID > 0 && Pattern.OrderTitle_ID < 1)
            //    {



            //        //Create List Of BranchORDER IDs

            //        List<long> orderIDs = new List<long>();
            //        var OrderTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder);

            //        if (OrderTempList.Count() > 0)
            //            orderIDs = OrderTempList.Select(f => f.TransactionType_ID).ToList();



            //        List<long> lackIDs = new List<long>();
            //        var LackTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder);

            //        if (LackTempList.Count() > 0)
            //            lackIDs = LackTempList.Select(f => f.TransactionType_ID).ToList();



            //        List<long> returnIDs = new List<long>();
            //        var ReturnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder);

            //        if (ReturnTempList.Count() > 0)
            //            returnIDs = ReturnTempList.Select(f => f.TransactionType_ID).ToList();



            //        //Check For OrderTitle_ID in BranchORDER

            //        if (Pattern.Type == BranchSoldType.Sold)
            //        {
            //            if (orderIDs.Count() > 0)
            //            {
            //                var orderTempList = Context.BranchOrders.Where(f => orderIDs.Contains(f.ID) && f.BranchOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));

            //                if (orderTempList.Count() > 0)
            //                {
            //                    orderIDs = orderTempList.Select(f => f.ID).ToList();
            //                    list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchOrder && orderIDs.Contains(f.TransactionType_ID)) ||
            //                                            (f.TransactionType != (int)BranchTransactionType.BranchOrder)).ToList();
            //                }
            //                else
            //                    list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchOrder).ToList();
            //            }



            //            if (lackIDs.Count() > 0)
            //            {
            //                var lackTempList = Context.BranchLackOrders.Where(f => lackIDs.Contains(f.ID) && f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));

            //                if (lackTempList.Count() > 0)
            //                {
            //                    lackIDs = lackTempList.Select(f => f.ID).ToList();
            //                    list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchLackOrder && lackIDs.Contains(f.TransactionType_ID)) ||
            //                                            (f.TransactionType != (int)BranchTransactionType.BranchLackOrder)).ToList();
            //                }
            //                else
            //                    list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchLackOrder).ToList();
            //            }

            //        }


            //        //var returnTempList = Context.BranchReturnOrders.Where(f => returnIDs.Contains(f.ID));
            //        var returnTempList = Context.BranchReturnOrders.Where(f => returnIDs.Contains(f.ID) && f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));

            //        if (returnTempList.Count() > 0)
            //        {
            //            returnIDs = returnTempList.Select(f => f.ID).ToList();
            //            list = list.Where(f => (f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && returnIDs.Contains(f.TransactionType_ID)) ||
            //                                    (f.TransactionType != (int)BranchTransactionType.BranchReturnOrder)).ToList();
            //        }
            //        else
            //            list = list.Where(f => f.TransactionType != (int)BranchTransactionType.BranchReturnOrder).ToList();

            //    }
            //}

            return list.AsQueryable();
        }

        public IQueryable<BranchSold> Search_SelectPrint(BranchSoldPattern Pattern)
        {
            return Search_Where(Pattern).OrderBy(f => f.DisplayOrder);
        }

        public IQueryable<BranchSold> CreateBranchSoldList(IQueryable<BranchTransaction> list, BranchSoldPattern Pattern)
        {

            //var _IDs=_list.Select(f=>f.ID).ToList();
            //var list = Context.BranchTransactions.Where(f => _IDs.Contains(f.ID));
            var BranchList = Context.Branches.ToList();

            if (list.Count() < 1)
                return new List<BranchSold>().AsQueryable();

            List<BranchSold> result = new List<BranchSold>();

            foreach (long Branch_ID in list.Select(f => f.Branch_ID).Distinct())
            {
                BranchSold temp = new BranchSold();

                temp.ID = Branch_ID;
                temp.Title = BranchList.Where(f => f.ID == Branch_ID).FirstOrDefault().Title;
                temp.MinimumCredit = BranchList.Where(f => f.ID == Branch_ID).FirstOrDefault().MinimumCredit;
                temp.DisplayOrder = BranchList.Where(f => f.ID == Branch_ID).FirstOrDefault().DispOrder;

                if (Pattern.Type == BranchSoldType.Sold)
                {
                    //if (Pattern.OrderTitle_ID < 1)
                    //{
                        //Order
                        long orderAmount = 0;
                        var orderTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder && f.Branch_ID == Branch_ID);
                        if (orderTempList.Count() > 0)
                            orderAmount = orderTempList.Sum(f => f.BranchDebt);

                        //Lack
                        long lackAmount = 0;
                        var lackTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder && f.Branch_ID == Branch_ID);
                        if (lackTempList.Count() > 0)
                            lackAmount = lackTempList.Sum(f => f.BranchCredit);

                        //Return
                        long returnAmount = 0;
                        var returnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && f.Branch_ID == Branch_ID);
                        if (returnTempList.Count() > 0)
                            returnAmount = returnTempList.Sum(f => f.BranchCredit);

                        temp.Amount = orderAmount - returnAmount - lackAmount;
                    //}
                    //else
                    //{
                    //    //Order
                    //    long orderAmount = 0;
                    //    var orderTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder && f.Branch_ID == Branch_ID);
                    //    if (orderTempList.Count() > 0)
                    //    {
                    //        var orderList = Context.BranchOrders.Where(f => orderTempList.Any(g => g.TransactionType_ID == f.ID));
                    //        IQueryable<BranchOrderDetail> OrderDetails = orderList.SelectMany(f => f.BranchOrderDetails).Where(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID);
                    //        if (OrderDetails.Count<BranchOrderDetail>() > 0)
                    //            orderAmount = OrderDetails.Sum(f => f.TotalPrice);
                    //    }


                    //    //Lack
                    //    long lackAmount = 0;
                    //    var lackTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder && f.Branch_ID == Branch_ID);
                    //    if (lackTempList.Count() > 0)
                    //    {
                    //        var lackList = Context.BranchLackOrders.Where(f => lackTempList.Any(g => g.TransactionType_ID == f.ID));
                    //        IQueryable<BranchLackOrderDetail> lackDetails = lackList.SelectMany(f => f.BranchLackOrderDetails).Where(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID);
                    //        if (lackDetails.Count<BranchLackOrderDetail>() > 0)
                    //            lackAmount = lackDetails.Sum(f => f.TotalPrice);
                    //    }

                    //    //Return
                    //    long returnAmount = 0;
                    //    var returnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && f.Branch_ID == Branch_ID);
                    //    if (returnTempList.Count() > 0)
                    //    {
                    //        var returnList = Context.BranchReturnOrders.Where(f => returnTempList.Any(g => g.TransactionType_ID == f.ID));
                    //        IQueryable<BranchReturnOrderDetail> returnDetails = returnList.SelectMany(f => f.BranchReturnOrderDetails).Where(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID);
                    //        if (returnDetails.Count<BranchReturnOrderDetail>() > 0)
                    //            returnAmount = returnDetails.Sum(f => f.TotalPrice);
                    //    }

                    //    temp.Amount = orderAmount - returnAmount - lackAmount;
                    //}
                }
                //If Rejected
                else
                {
                    //if (Pattern.OrderTitle_ID < 1)
                    //{
                        long returnAmount = 0;
                        var returnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && f.Branch_ID == Branch_ID);
                        if (returnTempList.Count() > 0)
                            returnAmount = returnTempList.Sum(f => f.BranchCredit);

                        temp.Amount = returnAmount;
                    //}
                    //else
                    //{
                    //    long returnAmount = 0;
                    //    var returnTempList = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder && f.Branch_ID == Branch_ID);
                    //    if (returnTempList.Count() > 0)
                    //    {
                    //        var returnList = Context.BranchReturnOrders.Where(f => returnTempList.Any(g => g.TransactionType_ID == f.ID));
                    //        var returnDetails = returnList.SelectMany(f => f.BranchReturnOrderDetails.Where(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                    //        if (returnDetails.Count() > 0)
                    //            returnAmount = returnTempList.Sum(f => f.BranchCredit);
                    //    }

                    //    temp.Amount = returnAmount;
                    //}
                }


                result.Add(temp);
            }

            return result.AsQueryable();
        }

        public HtmlTable CreateTableOfBranchSoldReadOnly(long Branch_ID, BranchSoldPattern Pattern)
        {
            Pattern.Branch_ID = Branch_ID;
            Pattern.OrderTitle_ID = 0;

            //1.Split Return & Order


            //IQueryable<BranchOrder> OrderList = Context.BranchOrders
            //                                               .Where(f => OrderTransactionList
            //                                                               .Where(d => d.TransactionType == (int)BranchTransactionType.BranchOrder)
            //                                                               .Any(h => h.TransactionType_ID == f.ID));

            //IQueryable<BranchLackOrder> LackList = Context.BranchLackOrders
            //                                                .Where(f => OrderTransactionList
            //                                                                .Where(d => d.TransactionType == (int)BranchTransactionType.BranchLackOrder)
            //                                                                .Any(h => h.TransactionType_ID == f.ID));

            //IQueryable<BranchReturnOrder> ReturnList = Context.BranchReturnOrders
            //                                               .Where(f => OrderTransactionList
            //                                                               .Where(d => d.TransactionType == (int)BranchTransactionType.BranchReturnOrder)
            //                                                               .Any(h => h.TransactionType_ID == f.ID));


            IQueryable<BranchOrder> OrderList = Context.BranchOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);

            IQueryable<BranchLackOrder> LackList = Context.BranchLackOrders.Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed);

            IQueryable<BranchReturnOrder> ReturnList = Context.BranchReturnOrders.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);

            bool isSold = Pattern.Type == BranchSoldType.Sold;

            {

                switch (Pattern.SoldPersianDate.SearchMode)
                {
                    case DateRangePattern.SearchType.Between:
                        if (Pattern.SoldPersianDate.HasFromDate && Pattern.SoldPersianDate.HasToDate)
                        {
                            if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
                            if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
                            ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
                        }
                        break;
                    case DateRangePattern.SearchType.Greater:
                        if (Pattern.SoldPersianDate.HasDate)
                        {
                            if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
                            if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
                            ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
                        }
                        break;
                    case DateRangePattern.SearchType.Less:
                        if (Pattern.SoldPersianDate.HasDate)
                        {
                            if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
                            if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
                            ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
                        }
                        break;
                    case DateRangePattern.SearchType.Equal:
                        if (Pattern.SoldPersianDate.HasDate)
                        {
                            if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
                            if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
                            ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
                        }
                        break;
                }



                switch (Pattern.Price.Type)
                {
                    case RangeType.Between:
                        if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
                        {
                            if (isSold) OrderList = OrderList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
                            if (isSold) LackList = LackList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
                            ReturnList = ReturnList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
                        }
                        break;
                    case RangeType.GreatherThan:
                        if (Pattern.Price.HasFirstNumber)
                        {
                            if (isSold) OrderList = OrderList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
                            if (isSold) LackList = LackList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
                            ReturnList = ReturnList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
                        }
                        break;
                    case RangeType.LessThan:
                        if (Pattern.Price.HasFirstNumber)
                        {
                            if (isSold) OrderList = OrderList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
                            if (isSold) LackList = LackList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
                            ReturnList = ReturnList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
                        }
                        break;
                    case RangeType.EqualTo:
                        if (Pattern.Price.HasFirstNumber)
                        {
                            if (isSold) OrderList = OrderList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
                            if (isSold) LackList = LackList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
                            ReturnList = ReturnList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
                        }
                        break;
                    case RangeType.Nothing:
                    default:
                        break;
                }



                if (Pattern.Branch_ID > 0)
                {
                    if (isSold) OrderList = OrderList.Where(f => f.Branch_ID == Pattern.Branch_ID);
                    if (isSold) LackList = LackList.Where(f => f.BranchOrder.Branch_ID == Pattern.Branch_ID);
                    ReturnList = ReturnList.Where(f => f.Branch_ID == Pattern.Branch_ID);
                }



                //if (Pattern.OrderTitle_ID > 0)
                //{
                //    if (isSold) OrderList = OrderList.Where(f => f.BranchOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //    if (isSold) LackList = LackList.Where(f => f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //    ReturnList = ReturnList.Where(f => f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));
                //}

                //if (Pattern.GroupTitle_ID > 0 && Pattern.OrderTitle_ID < 1)
                //{
                //    if (isSold) OrderList = OrderList.Where(f => f.BranchOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));
                //    if (isSold) LackList = LackList.Where(f => f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));
                //    ReturnList = ReturnList.Where(f => f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID));

                //}
            }


            //var OrderTransactionList = SearchDuringBranchTransactions(Pattern);
            bool hasOrder = OrderList.Count() > 0;
            bool hasLack = LackList.Count() > 0;
            bool hasReturn = ReturnList.Count() > 0;

            //2.Create List Of Order Title
            List<long> orderTitleIDs = Context.Branch_BranchOrderTitle.Where(f => f.Branch_ID == Branch_ID).Select(f => f.BranchOrderTitle_ID).ToList();

            List<BranchOrderDetail> OrderResult = new List<BranchOrderDetail>();
            if (hasOrder && isSold)
                OrderResult = OrderList.SelectMany(f => f.BranchOrderDetails).ToList();

            List<BranchLackOrderDetail> LackResult = new List<BranchLackOrderDetail>();
            if (hasOrder && isSold)
                LackResult = LackList.SelectMany(f => f.BranchLackOrderDetails).ToList();

            List<BranchReturnOrderDetail> ReturnResult = new List<BranchReturnOrderDetail>();
            if (hasOrder)
                ReturnResult = ReturnList.SelectMany(f => f.BranchReturnOrderDetails).ToList();


            //if (isSold)
            //    if (hasOrder)
            //        orderTitleIDs.AddRange(OrderList.SelectMany(f => f.BranchOrderDetails).Select(g => g.BranchOrderTitle_ID.Value));

            //if (isSold)
            //    if (hasLack)
            //        orderTitleIDs.AddRange(LackList.SelectMany(f => f.BranchOrder.BranchOrderDetails).Select(g => g.BranchOrderTitle_ID.Value));

            //if (hasReturn)
            //    orderTitleIDs.AddRange(ReturnList.SelectMany(f => f.BranchReturnOrderDetails).Select(g => g.BranchOrderTitle_ID.Value));

            orderTitleIDs = orderTitleIDs.Distinct().ToList();


            //Header
            HtmlTable table = new HtmlTable();
            HtmlTableRow hrow = new HtmlTableRow();
            hrow.Style.Add("font-weight", "bold");
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = (Pattern.Type == BranchSoldType.Rejected) ? "تعداد مرجوعی" : "تعداد فروخته شده" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل" });

            table.Rows.Add(hrow);

            long quantityOFAll = 0;
            long totalPriceOFAll = 0;

            //Add each BranchOrderTitle
            foreach (long orderTitle_ID in orderTitleIDs)
            {
                if (OrderResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID) &&
                    ReturnResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID) &&
                    LackResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID))
                    continue;


                HtmlTableRow row = new HtmlTableRow();



                //Cell 1
                row.Cells.Add(new HtmlTableCell() { InnerText = new BranchOrderTitleBusiness().Retrieve(orderTitle_ID).Title });

                //Cell 2

                #region Cell 2 Quantity

                long Quantity = 0;

                if (Pattern.Type == BranchSoldType.Sold && hasOrder)
                {
                    var _temp = OrderResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        Quantity += _temp.Sum(f => f.Quantity);
                }


                if (isSold && hasLack)
                {
                    var _temp = LackResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        Quantity = (Pattern.Type == BranchSoldType.Rejected) ?
                                            Quantity + _temp.Sum(f => f.Quantity)
                                            :
                                            Quantity - _temp.Sum(f => f.Quantity);
                }


                if (hasReturn)
                {
                    var _temp = ReturnResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        Quantity = (Pattern.Type == BranchSoldType.Rejected) ?
                                            Quantity + _temp.Sum(f => f.Quantity)
                                            :
                                            Quantity - _temp.Sum(f => f.Quantity);
                }

                HtmlTableCell quantityCell = new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(Quantity) + " عدد" };
                quantityCell.Attributes.Add("class", "totalspan");
                row.Cells.Add(quantityCell);


                //table.Rows.Add(row);

                #endregion




                //Cell 3

                #region Cell 3 TotalAmount

                long TotalAmount = 0;
                if (isSold && hasOrder)
                {
                    var _temp = OrderResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        TotalAmount += _temp.Sum(f => f.TotalPrice);
                }


                if (isSold && hasLack)
                {
                    var _temp = LackResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        TotalAmount = (Pattern.Type == BranchSoldType.Rejected) ?
                                            TotalAmount + _temp.Sum(f => f.TotalPrice)
                                            :
                                            TotalAmount - _temp.Sum(f => f.TotalPrice);
                }


                if (hasReturn)
                {
                    var _temp = ReturnResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                    if (_temp.Count() > 0)
                        TotalAmount = (Pattern.Type == BranchSoldType.Rejected) ?
                                            TotalAmount + _temp.Sum(f => f.TotalPrice)
                                            :
                                            TotalAmount - _temp.Sum(f => f.TotalPrice);
                }


                HtmlTableCell totalCell = new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(TotalAmount) + " ریال" };
                totalCell.Attributes.Add("class", "totalspan");
                row.Cells.Add(totalCell);

                #endregion


                table.Rows.Add(row);
                totalPriceOFAll += TotalAmount;
                quantityOFAll += Quantity;
            }

            #region Footer

            HtmlTableRow fRow = new HtmlTableRow();
            fRow.Attributes.Add("class", "footerRow");
            fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :" });
            fRow.Cells.Add(new HtmlTableCell() { InnerText = quantityOFAll.ToString() + " عدد" });
            fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(totalPriceOFAll) + "ریال" });
            table.Rows.Add(fRow);

            #endregion


            return table;
        }
    }



    public class BranchSold
    {
        public BranchSold()
        {
            Title = "";
        }
        public long ID { get; set; }
        public string Title { get; set; }
        public long MinimumCredit { get; set; }
        public long Amount { get; set; }
        public long DisplayOrder { get; set; }
    }
}
