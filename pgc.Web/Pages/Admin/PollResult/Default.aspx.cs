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
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.Model;

public partial class Pages_Admin_PollResult_Default : BasePage
{
    public PollResultBusiness result = new PollResultBusiness();
    public long PollID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HasValidQueryString<long>(QueryStringKeys.id))
        {
            PollID = GetQueryStringValue<long>(QueryStringKeys.id);
        }
   
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(GetRouteUrl("admin-poll",null));
        Response.RedirectToRoute("admin-poll", null);
    }
}