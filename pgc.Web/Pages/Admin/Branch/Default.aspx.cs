using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Admin_Branch_Default : BaseManagementPage<BranchBusiness, Branch, BranchPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new BranchBusiness();
    }

    public override void Command(object Sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pics")
        {
            long Branch_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            Response.Redirect(GetRouteUrl("admin-branchpic", null) + "?" + QueryStringKeys.id.ToString() + "=" + Branch_ID);
        }
        else if (e.CommandName == "ActiveRow")
        {
            long Branch_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            var op = this.Business.SetActiveOrDeactive(Branch_ID, true);
            foreach (var item in op.Messages)
                UserSession.AddMessage(item);

            this.ListControl.Grid.DataBind();
        }
        else if (e.CommandName == "DeActiveRow")
        {
            long Branch_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            var op = this.Business.SetActiveOrDeactive(Branch_ID, false);
            foreach (var item in op.Messages)
                UserSession.AddMessage(item);
            this.ListControl.Grid.DataBind();
        }
        base.Command(Sender, e);
    }

    //public override void Command(object Sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Pics")
    //    {

    //        long Branch_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
    //        Response.Redirect(GetRouteUrl("admin-branchpic",null) + "?" + QueryStringKeys.id.ToString() + "=" + Branch_ID);

           
    //    }

    //    base.Command(Sender, e);
    //}
}