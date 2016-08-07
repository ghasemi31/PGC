using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_SoldCostPieChart : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        string url = GetRouteUrl("admin-soldcostpie", null) + "?fromDate=" + fromDate + "&toDate=" + toDate + "&searchMode=" + pdrDate.DateRange.SearchMode;
        //Response.Redirect(url, "_blank", "menubar=0,scrollbars=1,width=2000,height=900,top=10");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", "window.open('" + url + "');", true);
    
    }
}