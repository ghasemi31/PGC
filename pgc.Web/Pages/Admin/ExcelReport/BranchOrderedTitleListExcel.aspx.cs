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

public partial class Pages_Admin_ExcelReport_BranchOrderedTitleListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchOrderedTitlePattern pattern = (BranchOrderedTitlePattern)Session["BranchOrderedTitlePrintPattern"];
            //BranchOrderedTitleBusiness business = new BranchOrderedTitleBusiness();
            //IQueryable<BranchOrderedTitle> OrderedsList = business.Search_SelectPrint(pattern);

            //DataTable table = new DataTable("Ordereds");

            //table.Columns.Add("ردیف");
            //table.Columns.Add("عنوان");
            //table.Columns.Add("نام گروه");
            //table.Columns.Add("تعداد");
            //table.Columns.Add("آخرین مبلغ تعیین شده");
            //table.Columns.Add("مبلغ کل");

            //int i=0;
            //foreach (var order in OrderedsList)
            //{
            //    i++;
            //    table.Rows.Add(
            //        i,
            //        order.Title,
            //        order.GroupTitle,
            //        UIUtil.GetCommaSeparatedOf(order.Quantity) + " عدد",
            //        UIUtil.GetCommaSeparatedOf(order.SinglePrice) + " ریال",
            //        UIUtil.GetCommaSeparatedOf(order.TotalPrice) + " ریال"                    
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