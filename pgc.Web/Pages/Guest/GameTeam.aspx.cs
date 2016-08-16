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
        if (order == null)
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

    protected void btnAddToGroup_Click(object sender, EventArgs e)
    {
        GameBusiness b = new GameBusiness();


        if (order.TeamMembers.Count() >= order.Game.GamerCount)
        {
            UserSession.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, string.Format("حداکثر تعداد بازیکن در ایم بازی {0} نفر می باشد", order.Game.GamerCount)));
        }
        else
        {
            OperationResult valRes = new OperationResult();

            TeamMember member = new TeamMember();
            member.FatherName = txtFather.Text;
            member.FullName = txtName.Text;
            member.NationalCode = txtNationalCode.Text;
            member.Order_ID = order.ID;

            var res = b.AddNewGamerToGroup(member);
            UserSession.AddMessage(res.Messages);

        }

        txtFather.Text = txtName.Text = txtNationalCode.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        OnlineGetway getway = (OnlineGetway)order.Getway_Enum;

                        switch (getway)
                        {
                            case OnlineGetway.MellatBankGateWay:
                                Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + order.ID);
                                break;
                            case OnlineGetway.AsanPardakhtGateWay:
                                Response.Redirect(GetRouteUrl("guest-asanonlinepayment", null) + "?id=" + order.ID);
                                break;
                            default:
                                break;
                        }
    }

}