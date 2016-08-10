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

public partial class Pages_Guest_GameDetail : BasePage
{
    GameBusiness business = new GameBusiness();
    public Game game;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<string>(QueryStringKeys.urlkey))
            Server.Transfer("~/Pages/Guest/404.aspx");
        game = business.RetriveGame(GetQueryStringValue_Routed<string>(QueryStringKeys.urlkey));
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
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }
}