using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using pgc.Business.General;
using kFrameWork.Model;
using pgc.Business;
using System.Text;
using System.IO;
using System.Net;
using pgc.Business.Core;

public partial class Pages_Guest_OnlinePayment : System.Web.UI.Page
{
    public BranchPayment online = new BranchPayment();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            online = new BranchPaymentBusiness().Retrieve(long.Parse(RouteData.Values["id"].ToString().Substring(1)));
            if (online.Type==(int)BranchPaymentType.Online && online.OnlineResultTransaction != 0)
                Response.Redirect(GetRouteUrl("agent-default", null));

            //lblTotalAmount.Text = UIUtil.GetCommaSeparatedOf(online.Order.TotalCost) + " ریال";

            btnSave.PostBackUrl = new SamanOnlinePayment().SamanUrl;
        }
        catch (Exception)
        {
            Response.Redirect(GetRouteUrl("agent-default", null));
        }

    }
}