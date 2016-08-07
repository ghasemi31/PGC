using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;

public partial class Pages_Admin_BranchReturnOrderTrust_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellLog = 9;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink h = new HyperLink();
            h.CssClass = "hbtn";
            h.Target= "_blank";
            h.NavigateUrl = ResolveClientUrl(GetRouteUrl("admin-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchReturnOrder");
            h.Text = "تاریخچه عملیات";

            e.Row.Cells[cellLog].Controls.Add(h);
        }
    }   
}