using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using kFrameWork.Util;
using pgc.Model.Enums;


public partial class Pages_Guest_HelpDetail :BasePage
{
    public HelpBusiness business = new HelpBusiness();
    public Help help;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        help = business.RetriveHelp(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (help == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}