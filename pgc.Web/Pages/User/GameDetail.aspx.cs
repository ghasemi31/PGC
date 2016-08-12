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

public partial class Pages_User_GameDetail : BasePage
{
    public GameOrder order;
    public long order_ID;
    public GameOrderBusiness business = new GameOrderBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {

        order_ID = GetQueryStringValue_Routed<long>(QueryStringKeys.id);
        order = business.RetriveGameOrder(order_ID);


    }


    protected void btnRemove_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            GameBusiness b = new GameBusiness();
            long userID = long.Parse(e.CommandArgument.ToString());
            var res = b.removeGamerFromGroup((long)order.Group_ID, userID);
            UserSession.AddMessage(res.Messages);
            lsvOrder.DataBind();
        }

    }

    protected void Btn_remove_Click(object sender, EventArgs e)
    {
        long userID = long.Parse(SelectedOrder.Value);

    }
    protected void Btn_Pay_Click(object sender, EventArgs e)
    {

        Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + order.ID);

    }

    protected void odsOrder_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["ID"] = order_ID;
    }

    protected void btnAddToGroup_Click(object sender, EventArgs e)
    {
        GameBusiness b = new GameBusiness();
        var user = b.RetriveGamer(txtNationalCode.Text);

        if (user == null)
        {

            UserSession.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, "بازیکنی با این کد ملی یافت نشد"));
        }
        else
        {

            if (order.Game != null && order.Game.Users.Any() && order.Game.Users.Count() >= order.Game.GamerCount)
            {
                UserSession.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, string.Format("حداکثر تعداد بازیکن در ایم بازی {0} نفر می باشد",order.Game.Users.Count())));
            }
            else
            {
                OperationResult valRes = new OperationResult();
                valRes = b.Validate(user.ID, (long)order.Game_ID);
                if (valRes.Result == ActionResult.Done)
                {
                    var res = b.AddNewGamerToGroup((long)order.Group_ID, user.ID);
                    UserSession.AddMessage(res.Messages);
                }
                else
                {
                    UserSession.AddCompeleteMessage(valRes.CompleteMessages);
                }
            }
        }
    }
}