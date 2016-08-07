using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace pgc.Business
{
    public class BranchOrderedTitleBusiness : BaseEntityManagementBusiness<BranchOrderTitle, pgcEntities>
    {
        public BranchOrderedTitleBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderedTitlePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Pattern);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchOrderedTitlePattern Pattern)
        {
            return Search_Where(Pattern).Count();
        }

        public IQueryable<BranchOrderedTitle> Search_Where(BranchOrderedTitlePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return new List<BranchOrderedTitle>().AsQueryable();



            IQueryable<BranchOrder> orders = Context.BranchOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);
            IQueryable<BranchReturnOrder> returnOrders = Context.BranchReturnOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                    }
                    break;
            }



            IQueryable<BranchOrderDetail> orderDetails = orders.SelectMany(f => f.BranchOrderDetails);
            IQueryable<BranchLackOrderDetail> lackDetails = orders.SelectMany(f => f.BranchLackOrders).Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed).SelectMany(f => f.BranchLackOrderDetails);
            IQueryable<BranchReturnOrderDetail> returnDetails = returnOrders.SelectMany(f => f.BranchReturnOrderDetails);


            if (orderDetails.Count() == 0 && returnDetails.Count() == 0)
                return new List<BranchOrderedTitle>().AsQueryable();

            bool hasLack = lackDetails.Count() > 0;
            bool hasReturn = returnDetails.Count() > 0;

            if (Pattern.OrderTitle_ID > 0)
            {
                orderDetails = orderDetails.Where(f => f.BranchOrderTitle_ID == Pattern.OrderTitle_ID);

                if (hasLack)
                    lackDetails = lackDetails.Where(f => f.BranchOrderTitle_ID == Pattern.OrderTitle_ID);

                if (hasReturn)
                    returnDetails = returnDetails.Where(f => f.BranchOrderTitle_ID == Pattern.OrderTitle_ID);
            }



            if (Pattern.GroupTitle_ID > 0)
            {
                orderDetails = orderDetails.Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID);

                if (hasLack)
                    lackDetails = lackDetails.Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID);

                if (hasReturn)
                    returnDetails = returnDetails.Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == Pattern.GroupTitle_ID);
            }

            if (Pattern.Branch_ID > 0)
            {
                orderDetails = orderDetails.Where(f => f.BranchOrder.Branch_ID == Pattern.Branch_ID);

                if (hasReturn)
                    returnDetails = returnDetails.Where(f => f.BranchReturnOrder.Branch_ID == Pattern.Branch_ID);

                if (hasLack)
                    lackDetails = lackDetails.Where(f => f.BranchLackOrder.BranchOrder.Branch_ID == Pattern.Branch_ID);
            }



            if (orderDetails.Count() == 0 && returnDetails.Count() == 0)
                return new List<BranchOrderedTitle>().AsQueryable();


            List<BranchOrderedTitle> result = new List<BranchOrderedTitle>();

            var OrderedTitleDistinct = orderDetails.Select(f => f.BranchOrderTitle).Union(returnDetails.Select(g => g.BranchOrderTitle)).Distinct();

            foreach (BranchOrderTitle orderTitle in OrderedTitleDistinct)
            {
                BranchOrderedTitle temp = new BranchOrderedTitle();
                temp.ID = orderTitle.ID;
                temp.Title = orderTitle.Title;
                temp.GroupTitle = orderTitle.BranchOrderTitleGroup.Title;
                temp.DisplayOrder = orderTitle.DisplayOrder;
                temp.GroupDisplayOrder = orderTitle.BranchOrderTitleGroup.DisplayOrder;

                var tempOrderDtl = orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Select(f => new { f.ID, f.SinglePrice });
                tempOrderDtl = tempOrderDtl.Union(returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Select(f => new { f.ID, f.SinglePrice }));

                temp.SinglePrice = tempOrderDtl.OrderByDescending(f => f.ID).FirstOrDefault().SinglePrice;

                switch (Pattern.Status)
                {
                    case BranchOrderedTitleStatus.Prepared:
                        if (orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() == 0)
                            continue;
                        temp.Quantity = orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                        temp.TotalPrice = orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);
                        break;


                    case BranchOrderedTitleStatus.Sold:

                        if (orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() == 0)
                            continue;

                        temp.Quantity = orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                        temp.TotalPrice = orderDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);

                        if (hasReturn && returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() > 0)
                        {
                            temp.Quantity = temp.Quantity - returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                            temp.TotalPrice = temp.TotalPrice - returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);
                        }


                        if (hasLack && lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() > 0)
                        {
                            temp.Quantity = temp.Quantity - lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                            temp.TotalPrice = temp.TotalPrice - lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);
                        }

                        break;


                    case BranchOrderedTitleStatus.Rejected:

                        if (hasReturn && returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() > 0)
                        {
                            temp.Quantity = returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                            temp.TotalPrice = returnDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);
                        }
                        else
                        {
                            temp.Quantity = 0;
                            temp.TotalPrice = 0;
                        }

                        break;

                    case BranchOrderedTitleStatus.Lacked:

                        if (hasLack && lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Count() > 0)
                        {
                            temp.Quantity = lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.Quantity);
                            temp.TotalPrice = lackDetails.Where(f => f.BranchOrderTitle_ID == orderTitle.ID).Sum(g => g.TotalPrice);
                        }
                        else
                        {
                            temp.Quantity = 0;
                            temp.TotalPrice = 0;
                        }
                        break;
                    default:
                        break;
                }

                if (temp.Quantity == 0)
                    continue;


                temp.TotalPrice = temp.SinglePrice * temp.Quantity;

                result.Add(temp);
            }

            return result.OrderBy(f => f.GroupDisplayOrder).ThenBy(f => f.DisplayOrder).AsQueryable();
        }

        public IQueryable<BranchOrderedTitle> Search_SelectPrint(BranchOrderedTitlePattern Pattern)
        {
            return Search_Where(Pattern);
        }

        public IQueryable<BranchDetailTitle> RetrieveOrderedTitle(long OrderTitle_ID, BranchOrderedTitlePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return null;



            IQueryable<BranchOrder> orders = Context.BranchOrders.Where(f => (f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized) && f.BranchOrderDetails.Any(g => g.BranchOrderTitle_ID == OrderTitle_ID));
            IQueryable<BranchReturnOrder> returnOrders = Context.BranchReturnOrders.Where(f => (f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized) && f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == OrderTitle_ID));

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                    }
                    break;
            }



            IQueryable<BranchOrderDetail> orderDetails = orders.SelectMany(f => f.BranchOrderDetails).Where(f => f.BranchOrderTitle_ID == OrderTitle_ID);
            IQueryable<BranchLackOrderDetail> lackDetails = orders.SelectMany(f => f.BranchLackOrders).Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed).SelectMany(f => f.BranchLackOrderDetails).Where(f => f.BranchOrderTitle_ID == OrderTitle_ID);
            IQueryable<BranchReturnOrderDetail> returnDetails = returnOrders.SelectMany(f => f.BranchReturnOrderDetails).Where(f => f.BranchOrderTitle_ID == OrderTitle_ID);


            if (orderDetails.Count() == 0 && returnDetails.Count() == 0)
                return null;


            bool hasLack = lackDetails.Count() > 0;
            bool hasReturn = returnDetails.Count() > 0;

            if (orderDetails.Count() == 0 && returnDetails.Count() == 0)
                return null;



            List<BranchDetailTitle> result = new List<BranchDetailTitle>();
            var branchList = orders.Select(f => f.Branch).Union(returnOrders.Select(g => g.Branch));

            foreach (var branch in branchList)
            {
                BranchDetailTitle temp = new BranchDetailTitle();
                temp.ID = branch.ID;
                temp.Title = branch.Title;
                temp.DisplayOrder = branch.DispOrder;

                var tempOrderDtl = orderDetails.Select(f => new { f.ID, f.SinglePrice });
                tempOrderDtl = tempOrderDtl.Union(returnDetails.Select(f => new { f.ID, f.SinglePrice }));
                temp.SinglePrice = tempOrderDtl.OrderByDescending(f => f.ID).FirstOrDefault().SinglePrice;

                var bo = orderDetails.Where(f => f.BranchOrder.Branch_ID == branch.ID);
                temp.OrderQuantity = (bo.Count() > 0) ? bo.Sum(f => f.Quantity) : 0;
                temp.OrderTotalPrice = (bo.Count() > 0) ? bo.Sum(f => f.TotalPrice) : 0;

                var lo = lackDetails.Where(f => f.BranchLackOrder.BranchOrder.Branch_ID == branch.ID);
                temp.LackQuantity = (lo.Count() > 0) ? bo.Sum(f => f.Quantity) : 0;
                temp.LackTotalPrice = (lo.Count() > 0) ? bo.Sum(f => f.TotalPrice) : 0;

                var ro = returnDetails.Where(f => f.BranchReturnOrder.Branch_ID == branch.ID);
                temp.LackQuantity = (ro.Count() > 0) ? ro.Sum(f => f.Quantity) : 0;
                temp.LackTotalPrice = (ro.Count() > 0) ? ro.Sum(f => f.TotalPrice) : 0;

                result.Add(temp);
            }


            return result.AsQueryable();
        }



        public long TotalPrice(BranchOrderedTitlePattern Pattern)
        {
            var result = Search_Where(Pattern);

            if (result.Count() > 0)
                return result.Sum(f => f.TotalPrice);
            else
                return 0;
        }

        public BranchOrderTitle RetrieveBranchOrderTitle(long OrderTitle_ID)
        {
            return Context.BranchOrderTitles.SingleOrDefault(f => f.ID == OrderTitle_ID);
        }

        public HtmlTable CreateBranchOrderedTitleBranchDetails(long OrderTitle_ID, DateRangePattern Pattern)
        {
            IQueryable<BranchOrder> orders = Context.BranchOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);
            IQueryable<BranchReturnOrder> returnOrders = Context.BranchReturnOrders.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);
            IQueryable<BranchLackOrder> lackOrder = Context.BranchLackOrders.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);
            switch (Pattern.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.ToDate) <= 0));
                    returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) >= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) >= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) <= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) <= 0));                    
                    }
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) == 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) == 0));                    
                    }
                    break;
            }


            IQueryable<BranchLackOrder> lackOrders = orders.SelectMany(f => f.BranchLackOrders).Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed);


            //Header
            HtmlTable table = new HtmlTable();
            HtmlTableRow hrow = new HtmlTableRow();
            hrow.Style.Add("font-weight", "bold");
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام شعبه" });

            HtmlTableCell WideCell = new HtmlTableCell() { InnerText = "تعداد ارسال شده به شعبه" };
            WideCell.Style.Add("width", "150px");
            hrow.Cells.Add(WideCell);

            hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد مرجوع شده" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد کل فروخته شده" });


            table.Rows.Add(hrow);

            //Add each BranchOrderTitle
            long PreparedTotalNumber = 0;
            long RejectedTotalNumber = 0;
            //long LackTotalNumber = 0;

            foreach (long bID in orders.OrderBy(f => f.Branch.DispOrder).Select(f => f.Branch_ID).Distinct())
            {
                HtmlTableRow row = new HtmlTableRow();

                //BranchName
                row.Cells.Add(new HtmlTableCell() { InnerText = orders.Where(f => f.Branch_ID == bID).FirstOrDefault().Branch.Title });


                //Part 1 Of Lack Number
                var lackList = lackOrders.Where(f => f.BranchOrder.Branch_ID == bID).SelectMany(f => f.BranchLackOrderDetails).Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                long lckNM = (lackList != null && lackList.Count() > 0) ? lackList.Sum(g => g.Quantity) : 0;

                //Prepared Number
                var preList = orders.Where(f => f.Branch_ID == bID).SelectMany(f => f.BranchOrderDetails).Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                long preNM = (preList != null && preList.Count() > 0) ? preList.Sum(g => g.Quantity) : 0;
                preNM -= lckNM;
                row.Cells.Add(new HtmlTableCell() { InnerText = preNM.ToString() });
                PreparedTotalNumber += preNM;


                //Part 2 Of Lack Number
                //row.Cells.Add(new HtmlTableCell() { InnerText = preNM.ToString() });
                //LackTotalNumber += lckNM;


                //Return Number
                long rejNM = 0;

                if (returnOrders != null && returnOrders.Count() > 0)
                {
                    var returnOrderDetailList = returnOrders.Where(f => f.Branch_ID == bID).SelectMany(g => g.BranchReturnOrderDetails);
                    if (returnOrderDetailList != null && returnOrderDetailList.Count() > 0)
                    {
                        var lastReturnList = returnOrderDetailList.Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                        rejNM = (lastReturnList != null && lastReturnList.Count() > 0) ? lastReturnList.Sum(g => g.Quantity) : 0;
                    }
                }
                row.Cells.Add(new HtmlTableCell() { InnerText = rejNM.ToString() });
                RejectedTotalNumber += rejNM;


                //Prepared Number
                row.Cells.Add(new HtmlTableCell() { InnerText = (preNM - rejNM).ToString() });


                table.Rows.Add(row);
            }

            #region Footer

            HtmlTableRow fRow = new HtmlTableRow();
            fRow.Attributes.Add("class", "footerRow");
            fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :" });
            fRow.Cells.Add(new HtmlTableCell() { InnerText = PreparedTotalNumber.ToString() });
            fRow.Cells.Add(new HtmlTableCell() { InnerText = RejectedTotalNumber.ToString() });
            fRow.Cells.Add(new HtmlTableCell() { InnerText = (PreparedTotalNumber - RejectedTotalNumber).ToString() });

            table.Rows.Add(fRow);

            #endregion


            return table;
        }


        public HtmlTable CreateBranchOrderedTitleNoMoneyBranchDetails(long OrderTitle_ID, DateRangePattern Pattern)
        {
            IQueryable<BranchOrder> orders = Context.BranchOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);
            IQueryable<BranchReturnOrder> returnOrders = Context.BranchReturnOrders.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);

            switch (Pattern.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.ToDate) <= 0));
                    returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) >= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) >= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) <= 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) <= 0));
                    }
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.HasDate)
                    {
                        orders = orders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) == 0));
                        returnOrders = returnOrders.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.Date) == 0));
                    }
                    break;
            }


            IQueryable<BranchLackOrder> lackOrders = orders.SelectMany(f => f.BranchLackOrders).Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed);


            //Header
            HtmlTable table = new HtmlTable();
            HtmlTableRow hrow = new HtmlTableRow();
            hrow.Style.Add("font-weight", "bold");
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام شعبه" });

            HtmlTableCell WideCell = new HtmlTableCell() { InnerText = "تعداد ارسال شده به شعبه" };
            WideCell.Style.Add("width", "150px");
            hrow.Cells.Add(WideCell);

            hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد مرجوع شده" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد کل فروخته شده" });

            table.Rows.Add(hrow);

            //Add each BranchOrderTitle
            long PreparedTotalNumber = 0;
            long RejectedTotalNumber = 0;
            //long LackTotalNumber = 0;

            foreach (long bID in orders.OrderBy(f => f.Branch.DispOrder).Select(f => f.Branch_ID).Distinct())
            {
                HtmlTableRow row = new HtmlTableRow();

                //BranchName
                row.Cells.Add(new HtmlTableCell() { InnerText = orders.Where(f => f.Branch_ID == bID).FirstOrDefault().Branch.Title });


                //Part 1 Of Lack Number
                var lackList = lackOrders.Where(f => f.BranchOrder.Branch_ID == bID).SelectMany(f => f.BranchLackOrderDetails).Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                long lckNM = (lackList != null && lackList.Count() > 0) ? lackList.Sum(g => g.Quantity) : 0;

                //Prepared Number
                var preList = orders.Where(f => f.Branch_ID == bID).SelectMany(f => f.BranchOrderDetails).Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                long preNM = (preList != null && preList.Count() > 0) ? preList.Sum(g => g.Quantity) : 0;
                preNM -= lckNM;
                row.Cells.Add(new HtmlTableCell() { InnerText = preNM.ToString() });
                PreparedTotalNumber += preNM;


                //Part 2 Of Lack Number
                //row.Cells.Add(new HtmlTableCell() { InnerText = preNM.ToString() });
                //LackTotalNumber += lckNM;


                //Return Number
                long rejNM = 0;

                if (returnOrders != null && returnOrders.Count() > 0)
                {
                    var returnOrderDetailList = returnOrders.Where(f => f.Branch_ID == bID).SelectMany(g => g.BranchReturnOrderDetails);
                    if (returnOrderDetailList != null && returnOrderDetailList.Count() > 0)
                    {
                        var lastReturnList = returnOrderDetailList.Where(g => g.BranchOrderTitle_ID == OrderTitle_ID);
                        rejNM = (lastReturnList != null && lastReturnList.Count() > 0) ? lastReturnList.Sum(g => g.Quantity) : 0;
                    }
                }
                row.Cells.Add(new HtmlTableCell() { InnerText = rejNM.ToString() });
                RejectedTotalNumber += rejNM;


                //Prepared Number
                row.Cells.Add(new HtmlTableCell() { InnerText = (preNM - rejNM).ToString() });


                table.Rows.Add(row);
            }

            //#region Footer

            //HtmlTableRow fRow = new HtmlTableRow();
            //fRow.Attributes.Add("class", "footerRow");
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :" });
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = PreparedTotalNumber.ToString() });
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = RejectedTotalNumber.ToString() });
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = (PreparedTotalNumber - RejectedTotalNumber).ToString() });

            //table.Rows.Add(fRow);

            //#endregion


            return table;
        }

    }






    public class BranchOrderedTitle
    {
        public BranchOrderedTitle()
        {
            Title = "";
            GroupTitle = "";
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public long Quantity { get; set; }
        public long SinglePrice { get; set; }
        public long TotalPrice { get; set; }
        public string GroupTitle { get; set; }
        public long DisplayOrder { get; set; }
        public long GroupDisplayOrder { get; set; }
    }


    public class BranchDetailTitle
    {
        public BranchDetailTitle()
        {
            Title = "";
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public long DisplayOrder { get; set; }

        public long LackQuantity { get; set; }
        public long OrderQuantity { get; set; }
        public long ReturnQuantity { get; set; }

        public long LackTotalPrice { get; set; }
        public long OrderTotalPrice { get; set; }
        public long ReturnTotalPrice { get; set; }

        public long SinglePrice { get; set; }
    }
}