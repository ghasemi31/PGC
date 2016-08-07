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

public partial class Pages_Admin_ExcelReport_BranchReturnOrderNoMoneyDetailExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchReturnOrderBusiness business = new BranchReturnOrderBusiness();


            BranchReturnOrder order = business.Retrieve(long.Parse(Request.QueryString["id"]));

            DataTable table = new DataTable("Orders");

            table.Columns.Add(" ");
            table.Columns.Add("  ");
            table.Columns.Add("   ");

            
            table.Rows.Add("کد مرجوعی", order.ID, "");
            table.Rows.Add("نام شعبه", order.Branch.Title, "");
            table.Rows.Add("تاریخ مرجوعی", Util.GetPersianDateWithTime(order.RegDate), "");
            table.Rows.Add("وضعیت",EnumUtil.GetEnumElementPersianTitle((BranchOrderStatus) order.Status), "");
            table.Rows.Add("توضیح مدیر", order.AdminDescription, "");
            table.Rows.Add("توضیح شعبه", order.BranchDescription, "");

            table.Rows.Add("", "", "");
            table.Rows.Add("ردیف", "نام کالا", "تعداد");
            int i=0;
            foreach (var dtl in order.BranchReturnOrderDetails)
            {
                i++;
                table.Rows.Add(
                    i,
                    dtl.BranchOrderTitle_Title,
                    UIUtil.GetCommaSeparatedOf(dtl.Quantity) + " عدد"
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