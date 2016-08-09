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


public partial class Pages_Guest_GameList : BasePage
{

    pgc.Business.General.GameBusiness business = new pgc.Business.General.GameBusiness();
    public IQueryable<Game> games; 
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!HasValidQueryString_Routed<int>(QueryStringKeys.id))
        //    Server.Transfer("~/Pages/Guest/404.aspx");
        //games =business.RetriveGameList(GetQueryStringValue_Routed<GameType>(QueryStringKeys.id));
        //if (games==null)
        //{
        //    Server.Transfer("~/Pages/Guest/404.aspx");
        //}
    }
}