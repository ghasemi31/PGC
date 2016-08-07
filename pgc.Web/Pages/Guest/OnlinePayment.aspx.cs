using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_OnlinePayment : BasePage
{
    public OnlinePayment online = new OnlinePayment();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.HasValidQueryString<long>(QueryStringKeys.id))
            Response.Redirect(GetRouteUrl("guest-default", null));
        else
        {
            online = new OnlinePaymentBusiness().Retrieve(this.GetQueryStringValue<long>(QueryStringKeys.id));

            if (online.Order.IsPaid || online.ResultTransaction != 0)
                Response.Redirect(GetRouteUrl("guest-default", null));

        }
    }
}