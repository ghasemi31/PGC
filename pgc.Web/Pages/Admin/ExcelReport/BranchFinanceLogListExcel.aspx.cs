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

public partial class Pages_Admin_ExcelReport_BranchFinanceLogListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchFinanceLogPattern pattern = (BranchFinanceLogPattern)Session["BranchFinanceLogPrintPattern"];
            BranchFinanceLogBusiness business = new BranchFinanceLogBusiness();
            IQueryable<BranchFinanceLog> FinanceLogsList = business.Search_SelectPrint(pattern);
            
            DataTable table = new DataTable("FinanceLogs");

            table.Columns.Add("ردیف");
            table.Columns.Add("نام شعبه");
            table.Columns.Add("نوع تغییر");
            table.Columns.Add("تغییر توسط");
            table.Columns.Add("تاریخ");
            table.Columns.Add("توضیحات");
            table.Columns.Add("سند مربوطه");


            int i=0;
            foreach (var order in FinanceLogsList)
            {
                i++;
                table.Rows.Add(
                    i,
                    order.BranchTitle,
                    EnumUtil.GetEnumElementPersianTitle((BranchFinanceLogActionType)order.ActionType),
                    order.UserName,
                    Util.GetPersianDateWithTime(order.Date),
                    order.Description,
                    EnumUtil.GetEnumElementPersianTitle((BranchFinanceLogType)order.LogType) + "(کد" + order.LogType_ID.ToString() + ")"
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