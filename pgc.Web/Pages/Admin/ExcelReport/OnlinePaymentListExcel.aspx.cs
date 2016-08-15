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

public partial class Pages_Admin_ExcelReport_OnlinePaymentListExcel : System.Web.UI.Page
{
    //public OnlinePaymentListBusiness paymentBusiness = new OnlinePaymentListBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

        //    OnlinePaymentListPattern pattern = (OnlinePaymentListPattern)Session["OnlinePaymentListPattern"];
        //    OnlinePaymentListBusiness business = new OnlinePaymentListBusiness();
        //    IQueryable<OnlinePaymentList> paymentList = business.Search_Select(pattern);


        //    DataTable table = new DataTable("Orders");

        //    table.Columns.Add("ردیف");
        //    table.Columns.Add("مبلغ");
        //    table.Columns.Add("کد سفارش");
        //    table.Columns.Add("رسید دیجیتالی");
        //    table.Columns.Add("تاریخ تراکنش");
        //    table.Columns.Add("وضعیت تراکنش");
        //    table.Columns.Add("پرداخت کننده");

        //    int i = 0;
        //    foreach (var item in paymentList)
        //    {
        //        long result = 0;
        //              string stateStr = "";
        //              OnlineTransactionStatus status = (OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), item.TransactionState.ToString());
        //              if (status != OnlineTransactionStatus.OK)
        //                  stateStr = EnumUtil.GetEnumElementPersianTitle(status);
        //              else
        //              {
        //                  result = long.Parse(item.ResultTransaction.ToString());
        //                  if (result > 0)
        //                      stateStr = "پرداخت شده";
        //                  else if (result == 0 || result < 20)
        //                      stateStr = "توضیحات موجود نیست";
        //                  else
        //                      stateStr = pgc.Business.UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString().Substring(1)));
        //              }


        //        string payment = "";
        //        string orderId = "--";
        //              if (!item.ResNum.ToString().Contains("b"))
        //              {
        //                  payment = "کاربر";
        //                  orderId = item.Order_ID.ToString();
        //              }
        //              else
        //              {
        //                  payment = paymentBusiness.RetriveBranchName(item.ResNum);
        //                  orderId = "--";
        //              }
                     

        //        i++;
        //        table.Rows.Add(
        //            i,
        //            UIUtil.GetCommaSeparatedOf(item.Amount)+" ریال",
        //            orderId,
        //            item.RefNum,
        //            pgc.Business.Util.GetPersianDateWithTime(item.Date),
        //            stateStr,
        //            payment
        //            );
        //    }


        //    DataSet dSet = new DataSet("table");
        //    dSet.Tables.Add(table);

        //    GridView gv = new GridView();
        //    gv.DataSource = dSet;
        //    gv.DataBind();

        //    ExportUtil.Export(filePath, gv, true);
        //}
        //catch (Exception ex)
        //{

        //}
    }
}