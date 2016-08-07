using System;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Agent_BranchFinanceLog_List : BaseListControl
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

        //btnBack.Visible =    (_Page.HasValidQueryString<long>(QueryStringKeys.fid) &&
        //                                _Page.HasValidQueryString<string>(QueryStringKeys.logtype));
    }

    //protected void BackBtn_Click(object sender, EventArgs e)
    //{
    //    var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

    //    string url = _Page.GetQueryStringValue<string>(QueryStringKeys.urlkey);

    //    switch (url)
    //    {
    //        case "BranchOrder":
    //            Response.Redirect(GetRouteUrl("agent-branchorder", null));
    //            break;
    //        case "BranchLackOrder":
    //            Response.Redirect(GetRouteUrl("agent-branchlackorder", null));
    //            break;
    //        case "BranchReturnOrder":
    //            Response.Redirect(GetRouteUrl("agent-branchreturnorder", null));
    //            break;

    //        default:
    //            break;
    //    }
    //}
}