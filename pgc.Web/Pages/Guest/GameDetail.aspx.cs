using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Business.Payment.OnlinePay;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_GameDetail : BasePage
{
    GameBusiness business = new GameBusiness();
    public Game game;
    public string redirectUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!HasValidQueryString_Routed<string>(QueryStringKeys.urlkey))
            Server.Transfer("~/Pages/Guest/404.aspx");
        string urlKey = GetQueryStringValue_Routed<string>(QueryStringKeys.urlkey);
        game = business.RetriveGame(urlKey);

        redirectUrl = "?redirecturl=" + GetRouteUrl("guest-gamedetail", new { urlkey = urlKey });

        if (game == null)
        {
            Server.Transfer("~/Pages/Guest/404.aspx");
        }
        if (UserSession.IsUserLogined)
        {
            mlvGame.ActiveViewIndex = 1;
        }
        else
        {
            mlvGame.ActiveViewIndex = 0;
        }

        //ClientScript.RegisterStartupScript(this.GetType(), "hash", " $(window).scrollTop(500);", true);
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        SubmitOrder();
    }



    public void SubmitOrder()
    {
        if (UserSession.IsUserLogined)
        {

            GameOrderBusiness orderBusiness = new GameOrderBusiness();
      
            OperationResult valRes = new OperationResult();
            valRes = business.Validate(UserSession.UserID, game.ID);

            if (valRes.Result == ActionResult.Done)
            {

                GameOrder model = new GameOrder();
                model.Game_ID = game.ID;
                model.User_ID = UserSession.UserID;
                if (game.GamerCount > 1)
                {
                    model.Group_ID = business.AddNewGameGroup(txtTeamName.Text);
                    business.AddNewGamerToGroup((long)model.Group_ID, (long)model.User_ID);
                }
                if (game.HowType_Enum == (int)GameHowType.Offline)
                {
                    long id = lkcGameCenetr.GetSelectedValue<long>();
                    GameCenter center = business.RetriveGameCenter(id);
                    model.GameCenter_ID = id;
                    model.GameCenterTitle = center.TItle;
                }

                OperationResult result = new OperationResult();
                result = orderBusiness.AddNewOrder(model);
                UserSession.AddMessage(result.Messages);

                if (result.Result == ActionResult.Done)
                {
                    long orderID = (long)result.Data["Order_ID"];
                    Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + orderID);

                }
    }
            else
            {
                UserSession.AddCompeleteMessage(valRes.CompleteMessages);
            }
        }
    }


    protected void lkcGameCenetr_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = lkcGameCenetr.GetSelectedValue<long>();
       GameCenter center= business.RetriveGameCenter(id);
       if (center != null)
       {
           lblDesc.Text = center.Description;
       }
    }
}