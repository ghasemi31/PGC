using System;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using System.Web.UI;

public partial class Pages_Admin_BranchFinanceLog_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

        //btnBack.Visible = (_Page.HasValidQueryString<long>(QueryStringKeys.fid) && 
        //                                _Page.HasValidQueryString<string>(QueryStringKeys.logtype));
    }

    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int branchTitleCell = 3;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[branchTitleCell].Text = DataBinder.Eval(e.Row.DataItem, "BranchTitle").ToString().Replace("شعبه ","");
        }

    }

    //protected void BackBtn_Click(object sender, EventArgs e)
    //{
    //    var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

    //    string url= _Page.GetQueryStringValue<string>(QueryStringKeys.urlkey);

    //    switch (url)
    //    {
    //        case "BranchOrder":
    //            Response.Redirect(GetRouteUrl("admin-branchorder", null));
    //            break;
    //        case "BranchOrderTrust":
    //            Response.Redirect(GetRouteUrl("admin-branchordertrust", null));
    //            break;
    //        case "BranchLackOrder":
    //            Response.Redirect(GetRouteUrl("admin-branchlackorder", null));
    //            break;
    //        case "BranchLackOrderTrust":
    //            Response.Redirect(GetRouteUrl("admin-branchlackordertrust", null));
    //            break;
    //        case "BranchReturnOrder":
    //            Response.Redirect(GetRouteUrl("admin-branchreturnorder", null));
    //            break;
    //        case "BranchReturnOrderTrust":
    //            Response.Redirect(GetRouteUrl("admin-branchreturnordertrust", null));
    //            break;

    //        default:
    //            break;
    //    }
    //}
}