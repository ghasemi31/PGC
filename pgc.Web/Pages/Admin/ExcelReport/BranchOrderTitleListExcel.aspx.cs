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

public partial class Pages_Admin_ExcelReport_BranchOrderTitleListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchOrderTitlePattern pattern = (BranchOrderTitlePattern)Session["BranchOrderTitlePrintPattern"];
            BranchOrderTitleBusiness business = new BranchOrderTitleBusiness();
            IQueryable<BranchOrderTitle> OrdersList = business.Search_SelectPrint(pattern);
            
            
            DataTable table = new DataTable("Orders");

            table.Columns.Add("ردیف");
            table.Columns.Add("نام کالا");
            table.Columns.Add("نام گروه");
            table.Columns.Add("مبلغ");
            table.Columns.Add("وضعیت");
            table.Columns.Add("اولویت نمایش");

            int i=0;
            foreach (var order in OrdersList.OrderBy(f => f.BranchOrderTitleGroup.DisplayOrder).ThenBy(f=>f.DisplayOrder))
            {
                i++;
                table.Rows.Add(
                    i,
                    order.Title,
                    order.BranchOrderTitleGroup.Title,
                    UIUtil.GetCommaSeparatedOf(order.Price) + " ریال",
                    EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus) order.Status),
                    order.DisplayOrder
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