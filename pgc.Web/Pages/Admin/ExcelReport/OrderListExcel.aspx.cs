using kFrameWork.Util;
using pgc.Business;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_ExcelReport_OrderListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            GameOrdersPattern pattern = (GameOrdersPattern)Session["GameOrderPattern"];
            GameOrdersBusiness business = new GameOrdersBusiness();
            IQueryable<GameOrder> OrdersList = business.Search_SelectPrint(pattern);


            DataTable table = new DataTable("Orders");

            table.Columns.Add("ردیف");
            table.Columns.Add("کد ثبت نام");
            table.Columns.Add("نام ثبت نام کننده");
            table.Columns.Add("مبلغ");
            table.Columns.Add("وضعیت پرداخت");
            table.Columns.Add("تاریخ ثبت نام");
            table.Columns.Add("بازی");





            int i = 0;
            foreach (var order in OrdersList.OrderByDescending(f => f.ID))
            {
                i++;
                table.Rows.Add(
                    i,
                    order.ID,
                    order.Name,
                    order.PayableAmount,
                    (order.IsPaid) ? "پرداخت شده" : "پرداخت نشده",
                    order.OrderPersianDate,
                    order.GameTitle
                    );
            }


            DataSet dSet = new DataSet("table");
            dSet.Tables.Add(table);

            GridView gv = new GridView();
            gv.DataSource = dSet;
            gv.DataBind();

            ExportUtil.Export(filePath, gv, true);
        }
        catch (Exception ex)
        {

        }
    }
}