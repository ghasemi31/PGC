using kFrameWork.UI;
using pgc.Business.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_GameList : System.Web.UI.Page
{
    private OrderBusiness business = new OrderBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.IsUserLogined)
        {
            Response.Redirect(GetRouteUrl("guest-login", null) + "?redirecturl=" + this.AppRelativeVirtualPath);
        }

    }
    protected void Btn_Pay_Click(object sender, EventArgs e)
    {
        //long orderID = long.Parse(SelectedOrder.Value);
        //string ResNum = new SamanOnlinePayment().CreateReservationNumber(orderID);
        //Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + ResNum);

    }
}