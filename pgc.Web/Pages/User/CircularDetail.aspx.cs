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

public partial class Pages_User_CircularDetail : BasePage
{
    public CircularBusiness business = new CircularBusiness();

    public Circular circular;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");
        circular = business.RetriveCircular(GetQueryStringValue_Routed<long>(QueryStringKeys.id));
        if (circular == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}