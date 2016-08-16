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

public partial class Pages_Admin_ExcelReport_BranchTransactionListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            //BranchTransactionPattern pattern = (BranchTransactionPattern)Session["BranchTransactionPrintPattern"];
            //BranchTransactionBusiness business = new BranchTransactionBusiness();
            //IQueryable<BranchTransaction> CreditsList = business.Search_SelectPrint(pattern);


            //DataTable table = new DataTable("Orders");

            //table.Columns.Add("ردیف");
            //table.Columns.Add("نام شعبه");
            //table.Columns.Add("نوع تراکنش");
            //table.Columns.Add("بستانکار");
            //table.Columns.Add("بدهکار");
            //table.Columns.Add("تاریخ");
            //table.Columns.Add("توضیحات");

            //int i=0;
            //foreach (var order in CreditsList)
            //{
            //    i++;
            //    table.Rows.Add(
            //        i,
            //        order.Branch.Title,                    
            //        EnumUtil.GetEnumElementPersianTitle((BranchTransactionType)order.TransactionType),
            //        UIUtil.GetCommaSeparatedOf(order.BranchCredit) + " ریال",
            //        UIUtil.GetCommaSeparatedOf(order.BranchDebt) + " ریال",
            //        Util.GetPersianDateWithTime(order.RegDate),
            //        order.Description
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