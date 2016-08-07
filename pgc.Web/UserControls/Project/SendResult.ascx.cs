using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Business.Core;
using kFrameWork.Util;
using kFrameWork.UI;

public partial class UserControls_Project_SendResult : BaseUserControl
{
    public SendSMSResult Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      //  UIUtil.AddCSSReference("~/Styles/UserControls/Project/SendResult.css", this, "DynamicHeader");
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (Result != null)
        {
            lblRes_SentMessagesCount.Text = UIUtil.GetCommaSeparatedOf(Result.Total_SucceedCount);
            lblRes_FailedMessagesCount.Text = UIUtil.GetCommaSeparatedOf(Result.Total_FailedCount);
            lblRes_UnknownMessagesCount.Text = UIUtil.GetCommaSeparatedOf(Result.Total_UnknownCount);
            lblRes_ErrorMessagesCount.Text = UIUtil.GetCommaSeparatedOf(Result.Total_ErrorCount);
            lblRes_TotalMessagesCount.Text = UIUtil.GetCommaSeparatedOf(Result.Total_SumCount);
           // long ChargeAmount = Convert.ToInt64(Result.ChargeAmount);
           // ChargeAmount = (ChargeAmount < 0) ? -1 * ChargeAmount : ChargeAmount;
            //lblRes_ChargeAmount.Text = UIUtil.GetCommaSeparatedOf(ChargeAmount) + " ریال";
            //lblRes_CurrentBalance.Text = UIUtil.GetCommaSeparatedOf(Convert.ToInt64(Result.CurrentBalance)) + " ریال";
        }
    }
}