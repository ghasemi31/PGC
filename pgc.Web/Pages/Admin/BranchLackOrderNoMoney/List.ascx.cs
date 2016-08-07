using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;

public partial class Pages_Admin_BranchLackOrderNoMoney_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellBranchOrder = 9;
        int cellLog = 8;
        int cellOrderedDate = 4;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink h2 = new HyperLink();
            h2.CssClass = "hbtn";
            h2.Target = "_blank";
            h2.NavigateUrl = ResolveClientUrl(GetRouteUrl("admin-branchordernomoney", null) + "?id=" + DataBinder.Eval(e.Row.DataItem, "BranchOrder_ID").ToString());
            h2.Text = "درخواست";
            e.Row.Cells[cellBranchOrder].Controls.Add(h2);


            HyperLink h1 = new HyperLink();
            h1.CssClass = "hbtn";
            h1.Target = "_blank";
            h1.NavigateUrl = ResolveClientUrl(GetRouteUrl("admin-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchLackOrder");
            h1.Text = "تاریخچه عملیات";
            e.Row.Cells[cellLog].Controls.Add(h1);


            e.Row.Cells[cellOrderedDate].Text = Util.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "RegDate"));
        }
    }
}