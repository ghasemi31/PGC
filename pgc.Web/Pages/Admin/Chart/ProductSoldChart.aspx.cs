using kFrameWork.UI;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_ProductSoldChart : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSoldAll_Click(object sender, EventArgs e)
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
        string url = GetRouteUrl("admin-chart-allproductsold", null) + "?fromDate=" + fromDate + "&toDate=" + toDate + "&searchMode=" + pdrDate.DateRange.SearchMode;
        //Response.Redirect(url, "_blank", "menubar=0,scrollbars=1,width=2000,height=900,top=10");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", "window.open('" + url + "');", true);
    }
    protected void btnBranchSold_Click(object sender, EventArgs e)
    {
        string fromDate = "", toDate = "";
        if (pdrBranchDate.DateRange.SearchMode == DateRangePattern.SearchType.Equal || pdrBranchDate.DateRange.SearchMode == DateRangePattern.SearchType.Greater || pdrBranchDate.DateRange.SearchMode == DateRangePattern.SearchType.Less)
        {
            fromDate = pdrBranchDate.DateRange.Date;
        }
        if (pdrBranchDate.DateRange.SearchMode == DateRangePattern.SearchType.Between)
        {
            fromDate = pdrBranchDate.DateRange.FromDate;
            toDate = pdrBranchDate.DateRange.ToDate;
        }
        string url = GetRouteUrl("admin-chart-branchsold", null) + "?fromDate=" + fromDate + "&toDate=" + toDate + "&searchMode=" + pdrBranchDate.DateRange.SearchMode + "&id=" + lkpBranch.GetSelectedValue<long>();
        //Response.Redirect(url, "_blank", "menubar=0,scrollbars=1,width=2000,height=900,top=10");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", "window.open('" + url + "');", true);
    }
    protected void btnProductSold_Click(object sender, EventArgs e)
    {
        string fromDate = "", toDate = "";
        if (pdrProductCase.DateRange.SearchMode == DateRangePattern.SearchType.Equal || pdrProductCase.DateRange.SearchMode == DateRangePattern.SearchType.Greater || pdrProductCase.DateRange.SearchMode == DateRangePattern.SearchType.Less)
        {
            fromDate = pdrProductCase.DateRange.Date;
        }
        if (pdrProductCase.DateRange.SearchMode == DateRangePattern.SearchType.Between)
        {
            fromDate = pdrProductCase.DateRange.FromDate;
            toDate = pdrProductCase.DateRange.ToDate;
        }
        string url = GetRouteUrl("admin-chart-productcasesold", null) + "?fromDate=" + fromDate + "&toDate=" + toDate + "&searchMode=" + pdrProductCase.DateRange.SearchMode + "&id=" + lkpProduct.GetSelectedValue<long>() + "&groupID=" + lkpProductGroup.GetSelectedValue<long>();
        //Response.Redirect(url, "_blank", "menubar=0,scrollbars=1,width=2000,height=900,top=10");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "script", "window.open('" + url + "');", true);
    }
}