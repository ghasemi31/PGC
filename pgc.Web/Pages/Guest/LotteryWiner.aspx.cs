using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;

public partial class Pages_Guest_LotteryList : BasePage
{
    public LotteryBusiness business = new LotteryBusiness();

    public Lottery Lottery;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        Lottery = business.RetriveLottery(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (Lottery == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}