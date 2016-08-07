using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_OnlinePayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.IsUserLogined)
        {
            Response.Redirect(GetRouteUrl("guest-login", null) + "?redirecturl=" + this.AppRelativeVirtualPath);
        }
    }

    public string test(long resultTransaction, string transactionState)
    {
        long result = 0;
        OnlineTransactionStatus status = (OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus),transactionState);
        if (status != OnlineTransactionStatus.OK)
            return EnumUtil.GetEnumElementPersianTitle(status);
        else
        {
            result = long.Parse(resultTransaction.ToString());
            if (result > 0)
                return  "پرداخت شده";
            else
                return UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString().Substring(1)));
        }
        //return "" ;
    }
}