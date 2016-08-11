using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_NewsDetail : BasePage
{
    public NewsBusiness business = new NewsBusiness();

    public News news;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        news = business.RetriveNews(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (news == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}