using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business;
using pgc.Model.Patterns;
using System.Collections;
using System.Data;
using kFrameWork.UI;

public partial class Pages_Admin_ExcelReport_BranchSoldDetailExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchSoldBusiness business = new BranchSoldBusiness();

            //BranchSoldPattern Pattern = (BranchSoldPattern)Session["BranchSoldPrintPattern"];

            //long bID = long.Parse(Request.QueryString["id"]);


            //DataTable table = new DataTable("Orders");

            //table.Columns.Add(" ");
            //table.Columns.Add("  ");
            //table.Columns.Add("   ");
            //table.Columns.Add("    ");


            //table.Rows.Add("نام شعبه", new BranchBusiness().Retrieve(bID).Title, "", "");

            //string dateRange = "";
            //dateRange += EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)Pattern.SoldPersianDate.SearchMode);
            //dateRange += Pattern.SoldPersianDate.HasDate ? Pattern.SoldPersianDate.Date : "";
            //dateRange += Pattern.SoldPersianDate.HasFromDate ? Pattern.SoldPersianDate.FromDate : "";
            //dateRange += Pattern.SoldPersianDate.HasToDate ? " و " + Pattern.SoldPersianDate.ToDate : "";
            //table.Rows.Add("تاریخ", dateRange, "", "");

            //table.Rows.Add("نوع گزارش", EnumUtil.GetEnumElementPersianTitle(Pattern.Type), "", "");


            //table.Rows.Add("", "", "", "");
            //table.Rows.Add("ردیف", "نام کالا", "تعداد", "مبلغ کل");
            //int i = 0;






            //IQueryable<BranchOrder> OrderList = business.Context.BranchOrders.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);

            //IQueryable<BranchLackOrder> LackList = business.Context.BranchLackOrders.Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed);

            //IQueryable<BranchReturnOrder> ReturnList = business.Context.BranchReturnOrders.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);

            //bool isSold = Pattern.Type == BranchSoldType.Sold;

            //{

            //    switch (Pattern.SoldPersianDate.SearchMode)
            //    {
            //        case DateRangePattern.SearchType.Between:
            //            if (Pattern.SoldPersianDate.HasFromDate && Pattern.SoldPersianDate.HasToDate)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
            //                if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
            //                ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.ToDate) <= 0));
            //            }
            //            break;
            //        case DateRangePattern.SearchType.Greater:
            //            if (Pattern.SoldPersianDate.HasDate)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
            //                if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
            //                ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) >= 0));
            //            }
            //            break;
            //        case DateRangePattern.SearchType.Less:
            //            if (Pattern.SoldPersianDate.HasDate)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
            //                if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
            //                ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) <= 0));
            //            }
            //            break;
            //        case DateRangePattern.SearchType.Equal:
            //            if (Pattern.SoldPersianDate.HasDate)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
            //                if (isSold) LackList = LackList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
            //                ReturnList = ReturnList.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.SoldPersianDate.Date) == 0));
            //            }
            //            break;
            //    }



            //    switch (Pattern.Price.Type)
            //    {
            //        case RangeType.Between:
            //            if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
            //                if (isSold) LackList = LackList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
            //                ReturnList = ReturnList.Where(f => (f.TotalPrice >= Pattern.Price.FirstNumber && f.TotalPrice <= Pattern.Price.SecondNumber));
            //            }
            //            break;
            //        case RangeType.GreatherThan:
            //            if (Pattern.Price.HasFirstNumber)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
            //                if (isSold) LackList = LackList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
            //                ReturnList = ReturnList.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
            //            }
            //            break;
            //        case RangeType.LessThan:
            //            if (Pattern.Price.HasFirstNumber)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
            //                if (isSold) LackList = LackList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
            //                ReturnList = ReturnList.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
            //            }
            //            break;
            //        case RangeType.EqualTo:
            //            if (Pattern.Price.HasFirstNumber)
            //            {
            //                if (isSold) OrderList = OrderList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
            //                if (isSold) LackList = LackList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
            //                ReturnList = ReturnList.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
            //            }
            //            break;
            //        case RangeType.Nothing:
            //        default:
            //            break;
            //    }



            //    if (Pattern.Branch_ID > 0)
            //    {
            //        if (isSold) OrderList = OrderList.Where(f => f.Branch_ID == Pattern.Branch_ID);
            //        if (isSold) LackList = LackList.Where(f => f.BranchOrder.Branch_ID == Pattern.Branch_ID);
            //        ReturnList = ReturnList.Where(f => f.Branch_ID == Pattern.Branch_ID);
            //    }
            //}


            ////var OrderTransactionList = SearchDuringBranchTransactions(Pattern);
            //bool hasOrder = OrderList.Count() > 0;
            //bool hasLack = LackList.Count() > 0;
            //bool hasReturn = ReturnList.Count() > 0;

            ////2.Create List Of Order Title
            //List<long> orderTitleIDs = business.Context.Branch_BranchOrderTitle.Where(f => f.Branch_ID == bID).Select(f => f.BranchOrderTitle_ID).ToList();

            //orderTitleIDs = orderTitleIDs.Distinct().ToList();

            //List<BranchOrderDetail> OrderResult = new List<BranchOrderDetail>();
            //if (hasOrder && isSold)
            //    OrderResult = OrderList.SelectMany(f => f.BranchOrderDetails).ToList();

            //List<BranchLackOrderDetail> LackResult = new List<BranchLackOrderDetail>();
            //if (hasOrder && isSold)
            //    LackResult = LackList.SelectMany(f => f.BranchLackOrderDetails).ToList();

            //List<BranchReturnOrderDetail> ReturnResult = new List<BranchReturnOrderDetail>();
            //if (hasOrder)
            //    ReturnResult = ReturnList.SelectMany(f => f.BranchReturnOrderDetails).ToList();



            //foreach (long orderTitle_ID in orderTitleIDs)
            //{
            //    if (OrderResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID) &&
            //        ReturnResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID) &&
            //        LackResult.All(f => f.BranchOrderTitle_ID != orderTitle_ID))
            //        continue;


            //    //Cell 2               
            //    long Quantity = 0;

            //    if (Pattern.Type == BranchSoldType.Sold && hasOrder)
            //    {
            //        var _temp = OrderResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            Quantity += _temp.Sum(f => f.Quantity);
            //    }


            //    if (isSold && hasLack)
            //    {
            //        var _temp = LackResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            Quantity = (Pattern.Type == BranchSoldType.Rejected) ?
            //                                Quantity + _temp.Sum(f => f.Quantity)
            //                                :
            //                                Quantity - _temp.Sum(f => f.Quantity);
            //    }


            //    if (hasReturn)
            //    {
            //        var _temp = ReturnResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            Quantity = (Pattern.Type == BranchSoldType.Rejected) ?
            //                                Quantity + _temp.Sum(f => f.Quantity)
            //                                :
            //                                Quantity - _temp.Sum(f => f.Quantity);
            //    }


            //    //Cell 3


            //    long TotalAmount = 0;
            //    if (isSold && hasOrder)
            //    {
            //        var _temp = OrderResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            TotalAmount += _temp.Sum(f => f.TotalPrice);
            //    }


            //    if (isSold && hasLack)
            //    {
            //        var _temp = LackResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            TotalAmount = (Pattern.Type == BranchSoldType.Rejected) ?
            //                                TotalAmount + _temp.Sum(f => f.TotalPrice)
            //                                :
            //                                TotalAmount - _temp.Sum(f => f.TotalPrice);
            //    }


            //    if (hasReturn)
            //    {
            //        var _temp = ReturnResult.Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

            //        if (_temp.Count() > 0)
            //            TotalAmount = (Pattern.Type == BranchSoldType.Rejected) ?
            //                                TotalAmount + _temp.Sum(f => f.TotalPrice)
            //                                :
            //                                TotalAmount - _temp.Sum(f => f.TotalPrice);
            //    }



            //    i++;
            //    table.Rows.Add(
            //        i,
            //        new BranchOrderTitleBusiness().Retrieve(orderTitle_ID).Title,
            //        UIUtil.GetCommaSeparatedOf(Quantity) + " عدد",
            //        UIUtil.GetCommaSeparatedOf(TotalAmount) + " ریال"
            //        );



            //}




            //DataSet dSet = new DataSet("table");
            //dSet.Tables.Add(table);

            //GridView gv = new GridView();
            //gv.DataSource = dSet;
            //gv.DataBind();

            //ExportUtil.Export(filePath, gv, true);
        }
        catch (Exception ex)
        {

        }
    }
}