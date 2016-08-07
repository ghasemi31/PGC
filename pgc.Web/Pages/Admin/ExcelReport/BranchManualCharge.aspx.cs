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

public partial class Pages_Admin_ExcelReport_BranchManualCharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchManualChargePattern pattern = (BranchManualChargePattern)Session["BranchManualChargePattern"];
            BranchManualChargeBusiness business = new BranchManualChargeBusiness();
            IQueryable<BranchTransaction> ManualChargeList = business.Search_SelectPrint(pattern);


            DataTable table = new DataTable("Orders");

            table.Columns.Add("ردیف");
            table.Columns.Add("نام شعبه");
            table.Columns.Add("تاریخ شارژ");
            table.Columns.Add("بستانکار");
            table.Columns.Add("بدهکار");
            //table.Columns.Add("مبلغ کسری");
            //table.Columns.Add("مبلغ درخواست");
            table.Columns.Add("توضیحات مدیر");

            int i=0;
            foreach (var manualcharge in ManualChargeList.OrderByDescending(f => f.ID))
            {
                i++;
                table.Rows.Add(
                    i,
                    manualcharge.Branch.Title,
                    Util.GetPersianDateWithTime(manualcharge.RegDate),
                    //UIUtil.GetCommaSeparatedOf(order.TotalPrice) + " ریال",
                    //UIUtil.GetCommaSeparatedOf(order.BranchOrder.TotalPrice) + " ریال",
                    UIUtil.GetCommaSeparatedOf(manualcharge.BranchCredit) + "ریال",
                    UIUtil.GetCommaSeparatedOf(manualcharge.BranchDebt) + "ریال",
                    manualcharge.Description
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