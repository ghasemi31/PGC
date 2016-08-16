using kFrameWork.Model;
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

public partial class Pages_Guest_OrderTracking : BasePage
{
    public GameOrder order;
    public long order_ID;
    public GameOrderBusiness business = new GameOrderBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {

        order_ID = GetQueryStringValue_Routed<long>(QueryStringKeys.id);
        order = business.RetriveGameOrder(order_ID);

        if(order==null)
            Server.Transfer("~/Pages/Guest/404.aspx");

        if (!UserSession.IsUserLogined)
        {
            UserSession.AddMessage(UserMessageKey.AccessDenied);
            Server.Transfer("~/Pages/Guest/404.aspx");
        }

        if (UserSession.UserID != order.User_ID)
        {
            UserSession.AddMessage(UserMessageKey.AccessDenied);
            Server.Transfer("~/Pages/Guest/404.aspx");
        }
    }




    protected void odsOrder_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["ID"] = order_ID;
    }

   
}