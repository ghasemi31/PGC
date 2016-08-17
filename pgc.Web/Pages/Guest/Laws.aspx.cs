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

public partial class Pages_Guest_Laws :BasePage
{
    GameBusiness business = new GameBusiness();
    public Game game;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<int>(QueryStringKeys.urlkey))
            Server.Transfer("~/Pages/Guest/404.aspx");

        game = business.RetriveGame(GetQueryStringValue_Routed<string>(QueryStringKeys.urlkey));
        if (game == null)
        {
            Server.Transfer("~/Pages/Guest/404.aspx");
        }
    }
}