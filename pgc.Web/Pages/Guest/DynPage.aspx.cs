using kFrameWork.UI;
//using pgc.Business;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Business.General;

public partial class Pages_Guest_DynPage : BasePage
{
    public DynPageBusiness business = new DynPageBusiness();

    public DynPage portal;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<string>(QueryStringKeys.urlkey))
            Server.Transfer("~/Pages/Guest/404.aspx");

        portal = business.RetriveDynPage(GetQueryStringValue_Routed<string>(QueryStringKeys.urlkey));

        if (portal == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}