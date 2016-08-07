using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_BranchSummaryChart : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DateRangePattern Daterange = new DateRangePattern();
        //Daterange.FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-31));
        //Daterange.ToDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-1));
        //Daterange.SearchMode = DateRangePattern.SearchType.Between;
        //pdrDate.DateRange = Daterange;
        txtPrice.SetNumber(300000);
    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        long price = 0;
        if (chbMinPrice.Checked)
        {
            price = txtPrice.GetNumber<long>();
        }

        string fromDate = "", toDate = "";
        if (pdrDate.DateRange.SearchMode == DateRangePattern.SearchType.Equal || pdrDate.DateRange.SearchMode == DateRangePattern.SearchType.Greater || pdrDate.DateRange.SearchMode == DateRangePattern.SearchType.Less)
        {
            fromDate = pdrDate.DateRange.Date;
        }
        if (pdrDate.DateRange.SearchMode == DateRangePattern.SearchType.Between)
        {
            fromDate = pdrDate.DateRange.FromDate;
            toDate = pdrDate.DateRange.ToDate;
        }
        string url = GetRouteUrl("admin-chart-branchsummary", null) + "?fromDate=" + fromDate + "&toDate=" + toDate + "&searchMode=" + pdrDate.DateRange.SearchMode+"&minPrice="+price;
        //Response.Redirect(url, "_blank", "menubar=0,scrollbars=1,width=2000,height=900,top=10");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", "window.open('" + url + "');", true);
    }
}