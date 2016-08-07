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

public partial class Pages_Admin_ExcelReport_BranchOrderedTitleDetailExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchOrderedTitleBusiness business = new BranchOrderedTitleBusiness();
            BranchOrderedTitlePattern pattern = (BranchOrderedTitlePattern)Session["BranchOrderedTitlePrintPattern"];

            var orderDetailList = business.RetrieveOrderedTitle(long.Parse(Request.QueryString["id"]), pattern);
            BranchOrderTitle ttl = business.RetrieveBranchOrderTitle(long.Parse(Request.QueryString["id"]));

            DataTable table = new DataTable("Orders");

            table.Columns.Add(" ");
            table.Columns.Add("  ");
            table.Columns.Add("   ");
            table.Columns.Add("    ");
            table.Columns.Add("     ");
            table.Columns.Add("      ");
            table.Columns.Add("       ");
            table.Columns.Add("        ");
            table.Columns.Add("         ");
            table.Columns.Add("          ");



            table.Rows.Add("عنوان کالا", ttl.Title, "", "", "", "", "", "", "", "");
            table.Rows.Add("عنوان گروه", ttl.BranchOrderTitleGroup.Title, "", "", "", "", "", "", "", "");
            table.Rows.Add("مبلغ واحد", UIUtil.GetCommaSeparatedOf(ttl.Price) + " ریال", "", "", "", "", "", "", "", "");
            table.Rows.Add("وضعیت", EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)ttl.Status), "", "", "", "", "", "", "", "");


            table.Rows.Add("", "", "", "", "", "", "", "", "", "");

            table.Rows.Add("ردیف", "نام شعبه", "مبلغ واحد", "تعداد درخواستی", "مبلغ درخواستی", "تعداد کسری", "مبلغ کسری", "تعداد مرجوعی", "مبلغ مرجوعی", "مبلغ کل");

            int i=0;
            foreach (var dtl in orderDetailList.OrderBy(f=>f.DisplayOrder))
            {
                i++;
                table.Rows.Add(
                    i,
                    dtl.Title,
                    UIUtil.GetCommaSeparatedOf(dtl.SinglePrice) + " ریال",

                    UIUtil.GetCommaSeparatedOf(dtl.OrderQuantity) + " عدد",
                    UIUtil.GetCommaSeparatedOf(dtl.OrderTotalPrice) + " ریال",

                    UIUtil.GetCommaSeparatedOf(dtl.LackQuantity) + " عدد",
                    UIUtil.GetCommaSeparatedOf(dtl.LackTotalPrice) + " ریال",

                    UIUtil.GetCommaSeparatedOf(dtl.ReturnQuantity) + " عدد",
                    UIUtil.GetCommaSeparatedOf(dtl.ReturnTotalPrice) + " ریال",

                    UIUtil.GetCommaSeparatedOf(dtl.OrderTotalPrice - dtl.LackTotalPrice - dtl.ReturnTotalPrice) + " ریال"
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