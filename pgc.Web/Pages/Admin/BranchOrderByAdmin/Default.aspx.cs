using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Model;
using pgc.Business.Core;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business;
using System.Web.UI.HtmlControls;
using kFrameWork.Business;
using System.Web.Services;

public partial class Pages_Admin_BranchOrderByAdmin_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-adminbranchorder", null) + "?id=" + lkpGroup.GetSelectedValue<long>());
    }
}