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

public partial class Pages_Admin_ExcelReport_BranchLackOrderDetailExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchLackOrderBusiness business = new BranchLackOrderBusiness();


            //BranchLackOrder order = business.Retrieve(long.Parse(Request.QueryString["id"]));

            //DataTable table = new DataTable("Orders");

            //table.Columns.Add(" ");
            //table.Columns.Add("  ");
            //table.Columns.Add("   ");
            //table.Columns.Add("    ");
            //table.Columns.Add("     ");


            //table.Rows.Add("کد کسری", order.ID, "", "", "");
            //table.Rows.Add("کد درخواست", order.BranchOrder_ID, "", "", "");
            //table.Rows.Add("نام شعبه", order.BranchOrder.Branch.Title, "", "", "");
            //table.Rows.Add("تاریخ تحویل", order.OrderedPersianDate, "", "", "");
            //table.Rows.Add("تاریخ درج", Util.GetPersianDateWithTime(order.RegDate), "", "", "");
            //table.Rows.Add("وضعیت",EnumUtil.GetEnumElementPersianTitle((BranchLackOrderStatus) order.Status), "", "", "");
            //table.Rows.Add("مبلغ کل",UIUtil.GetCommaSeparatedOf(order.TotalPrice)+" ریال", "", "", "");
            //table.Rows.Add("توضیح مدیر", order.AdminDescription, "", "", "");
            //table.Rows.Add("توضیح شعبه", order.BranchDescription, "", "", "");

            //table.Rows.Add("", "", "", "", "");
            //table.Rows.Add("ردیف", "نام کالا", "تعداد", "مبلغ واحد", "مبلغ کل");
            //int i=0;
            //foreach (var dtl in order.BranchLackOrderDetails)
            //{
            //    i++;
            //    table.Rows.Add(
            //        i,
            //        dtl.BranchOrderTitle_Title,
            //        UIUtil.GetCommaSeparatedOf(dtl.Quantity) + " عدد",
            //        UIUtil.GetCommaSeparatedOf(dtl.SinglePrice) + " ریال",
            //        UIUtil.GetCommaSeparatedOf(dtl.TotalPrice) + " ریال"                   
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