using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;

public partial class Pages_Agent_BranchLackOrder_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var _Page=this.Page as BaseManagementPage<BranchLackOrderBusiness, BranchLackOrder, BranchLackOrderPattern, pgcEntities>;

        int orderCell = 10;
        int logCell = 9;
        int deleteCell = 11;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink h = new HyperLink();
            h.CssClass = "hbtn";
            h.Target = "_blank";
            h.NavigateUrl = ResolveClientUrl(GetRouteUrl("agent-branchorder", null) + "?id=" + DataBinder.Eval(e.Row.DataItem, "BranchOrder_ID").ToString());
            h.Text = "درخواست";

            e.Row.Cells[orderCell].Controls.Add(h);


            HyperLink h2 = new HyperLink();
            h2.CssClass = "hbtn";
            h2.Target = "_blank";
            h2.NavigateUrl = ResolveClientUrl(GetRouteUrl("agent-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchLackOrder");
            h2.Text = "تاریخچه عملیات";

            e.Row.Cells[logCell].Controls.Add(h2);

            e.Row.Cells[deleteCell].Controls[0].Visible = _Page.Business.IsOpenForAgentAction((long)DataBinder.Eval(e.Row.DataItem, "ID"));
        }

    }
}