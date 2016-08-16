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

public partial class Pages_Admin_ExcelReport_BranchOrderedTitleNoMoneyDetailExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchOrderedTitleBusiness business = new BranchOrderedTitleBusiness();
            //BranchOrderedTitlePattern pattern = (BranchOrderedTitlePattern)Session["BranchOrderedTitlePrintPattern"];

            //var orderDetailList = business.RetrieveOrderedTitle(long.Parse(Request.QueryString["id"]), pattern);
            //BranchOrderTitle ttl = business.RetrieveBranchOrderTitle(long.Parse(Request.QueryString["id"]));

            //DataTable table = new DataTable("Orders");

            //table.Columns.Add(" ");
            //table.Columns.Add("  ");
            //table.Columns.Add("   ");
            //table.Columns.Add("    ");
            //table.Columns.Add("     ");


            //table.Rows.Add("عنوان کالا", ttl.Title, "", "", "");
            //table.Rows.Add("عنوان گروه", ttl.BranchOrderTitleGroup.Title, "", "", "");
            //table.Rows.Add("وضعیت", EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)ttl.Status), "", "", "");


            //table.Rows.Add("", "", "", "", "");

            //table.Rows.Add("ردیف", "نام شعبه", "تعداد درخواستی", "تعداد کسری", "تعداد مرجوعی");

            //int i=0;
            //foreach (var dtl in orderDetailList.OrderBy(f=>f.DisplayOrder))
            //{
            //    i++;
            //    table.Rows.Add(
            //        i,

            //        dtl.Title,

            //        UIUtil.GetCommaSeparatedOf(dtl.OrderQuantity) + " عدد",

            //        UIUtil.GetCommaSeparatedOf(dtl.LackQuantity) + " عدد",

            //        UIUtil.GetCommaSeparatedOf(dtl.ReturnQuantity) + " عدد"
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