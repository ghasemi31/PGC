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

public partial class Pages_Admin_ExcelReport_BranchSoldListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchSoldPattern pattern = (BranchSoldPattern)Session["BranchSoldPrintPattern"];
            //BranchSoldBusiness business = new BranchSoldBusiness();
            //IQueryable<BranchSold> SoldsList = business.Search_SelectPrint(pattern);


            //DataTable table = new DataTable("Solds");

            //table.Columns.Add("ردیف");
            //table.Columns.Add("نام شعبه");
            //table.Columns.Add("مبلغ");
            //table.Columns.Add("سقف حد اقل اعتبار شعبه");

            //int i=0;
            //foreach (var order in SoldsList)
            //{
            //    i++;
            //    table.Rows.Add(
            //        i,
            //        order.Title,
            //        UIUtil.GetCommaSeparatedOf(order.Amount) + " ریال",
            //        UIUtil.GetCommaSeparatedOf(order.MinimumCredit) + " ریال"
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