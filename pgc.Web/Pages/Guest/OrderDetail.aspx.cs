using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business.Core;

public partial class Pages_Guest_OrderDetail : BasePage
{
    public OrderBusiness business = new OrderBusiness();

    public Order order;
    public List<OrderDetail> orderDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        order = business.RetriveOrder(GetQueryStringValue_Routed<long>(QueryStringKeys.id));
        orderDetail = business.GetOrderDetail(order.ID);
        if (order == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
        if (order.User_ID != UserSession.UserID)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }

    protected void Btn_Pay_Click(object sender, EventArgs e)
    {
        long orderID = long.Parse(SelectedOrder.Value);
        string ResNum = new SamanOnlinePayment().CreateReservationNumber(orderID);
        Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + ResNum);

    }
}