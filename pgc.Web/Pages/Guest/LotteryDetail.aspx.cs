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

public partial class Pages_Guest_LotteryDetail : BasePage
{
    public LotteryBusiness business = new LotteryBusiness();
  
    public Lottery Lottery;
    LotteryDetail detail = new LotteryDetail();
    OperationResult result = new OperationResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        Lottery = business.RetriveLottery(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (Lottery == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int lotteryCode=0;
        int.TryParse(txtCode.Value, out lotteryCode);
        detail.Code = lotteryCode;
        //detail.Code = txtCode.GetNumber<int>();
        detail.Description = txtComment.InnerText;
        detail.Email = txtEmail.Value;
        detail.FName = txtFName.Value;
        detail.LName = txtLName.Value;
        detail.LotteryID = Lottery.ID;

        result=business.RegisterLotteryDetail(detail);

        UserSession.AddMessage(result.Messages);

        if (result.Result == ActionResult.Done)
        {

                Response.Redirect(GetRouteUrl("guest-lotterylist", null));
        }
    }

}