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

public partial class Pages_Admin_ExcelReport_BranchReturnOrderListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchReturnOrderPattern pattern = (BranchReturnOrderPattern)Session["BranchReturnPrintPattern"];
            BranchReturnOrderBusiness business = new BranchReturnOrderBusiness();
            IQueryable<BranchReturnOrder> OrdersList = business.Search_SelectPrint(pattern);
                    
            
            DataTable table = new DataTable("Orders");

            table.Columns.Add("ردیف");
            table.Columns.Add("نام شعبه");
            table.Columns.Add("کد مرجوعی");
            table.Columns.Add("تاریخ مرجوعی");
            table.Columns.Add("وضعیت");
            table.Columns.Add("مبلغ");
            table.Columns.Add("توضیحات مدیر");
            table.Columns.Add("توضیحات شعبه");

            int i=0;
            foreach (var order in OrdersList.OrderByDescending(f=>f.ID))
            {
                i++;
                table.Rows.Add(
                    i,
                    order.Branch.Title,
                    order.ID,
                    Util.GetPersianDateWithTime(order.RegDate),
                    EnumUtil.GetEnumElementPersianTitle((BranchReturnOrderStatus)order.Status),
                    UIUtil.GetCommaSeparatedOf(order.TotalPrice) + " ریال",
                    order.AdminDescription,
                    order.BranchDescription
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