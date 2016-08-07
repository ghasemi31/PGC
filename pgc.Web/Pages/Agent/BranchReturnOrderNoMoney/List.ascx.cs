using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;

public partial class Pages_Agent_BranchReturnOrderNoMoney_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var _Page=this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;

        int deleteCell=9;
        int orderDateCell = 3;
        int logCell = 8;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[orderDateCell].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "RegDate"));
            e.Row.Cells[deleteCell].Controls[0].Visible = _Page.Business.IsOpenForAgentAction((long)DataBinder.Eval(e.Row.DataItem, "ID"));


            HyperLink h = new HyperLink();
            h.CssClass = "hbtn";
            h.Target = "_blank";
            h.NavigateUrl = ResolveClientUrl(GetRouteUrl("agent-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchReturnOrder");
            h.Text = "تاریخچه عملیات";
            e.Row.Cells[logCell].Controls.Add(h);

        }
    }
}