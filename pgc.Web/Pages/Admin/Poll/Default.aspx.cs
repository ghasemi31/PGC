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

public partial class Pages_Admin_Poll_Default : BaseManagementPage<PollBusiness, Poll, PollPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new PollBusiness();
    }

    public override void Command(object Sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Choises")
        {
            long Poll_ID =ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            Response.Redirect(GetRouteUrl("admin-pollchoise", null) + "?" + QueryStringKeys.id.ToString() + "=" + Poll_ID);
        }

        if (e.CommandName == "Result")
        {
            long Poll_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            Response.Redirect(GetRouteUrl("admin-pollresult", null) + "?" + QueryStringKeys.id.ToString() + "=" + Poll_ID);
        }
        base.Command(Sender, e);
    }

}