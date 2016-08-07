using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;

public partial class Pages_Agent_BranchOrder_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {        
        int cellDeliver = 5;
        int cellToLack = 10;
        int cellLog = 9;
        int cellDetele=11;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime deliverDate = DateUtil.GetEnglishDateTime(DataBinder.Eval(e.Row.DataItem, "OrderedPersianDate").ToString());
            e.Row.Cells[cellDeliver].Text = DateUtil.GetPersianDateWithTime(deliverDate);
                
            if ((bool)DataBinder.Eval(e.Row.DataItem, "HasLack"))
            {
                HyperLink h1 = new HyperLink();
                h1.CssClass = "hbtn";
                h1.Target = "_blank";
                h1.NavigateUrl = ResolveClientUrl(GetRouteUrl("agent-branchlackorder", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString());
                h1.Text = "کسری";

                e.Row.Cells[cellToLack].Controls.Add(h1);               
            }


            HyperLink h = new HyperLink();
            h.CssClass = "hbtn";
            h.Target = "_blank";
            h.NavigateUrl = ResolveClientUrl(GetRouteUrl("agent-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchOrder" );
            h.Text = "تاریخچه عملیات";
            e.Row.Cells[cellLog].Controls.Add(h);


            var _Page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;

            e.Row.Cells[cellDetele].Controls[0].Visible = _Page.Business.IsOpenForAgentAction((long)DataBinder.Eval(e.Row.DataItem, "ID"));

        }
    }
}