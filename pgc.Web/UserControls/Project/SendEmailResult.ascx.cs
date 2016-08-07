using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using kFrameWork.UI;
using pgc.Business;

public partial class UserControls_Project_SendEmailResult : BaseUserControl
{
    public SendEmailResult Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (Result != null)
        {
            if (Result.SentEmailCount == Result.TotalEmailCount)
            {
                lblRes_SentEmailCount.Text = UIUtil.GetCommaSeparatedOf(Result.SentEmailCount);
                lblRes_TotalMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.TotalEmailCount);
            }
            else if ((Result.SentEmailCount < Result.TotalEmailCount) && (0 < Result.SentEmailCount))
            {
                lblRes_BlockSize.Text = UIUtil.GetCommaSeparatedOf(Result.BlockSize);
                lblRes_FailedBlockCount.Text = UIUtil.GetCommaSeparatedOf(Result.FailedBlockCount);
                lblRes_FailedEmailCount.Text = UIUtil.GetCommaSeparatedOf(Result.FailedEmailCount);
                lblRes_SentBlockCount.Text = UIUtil.GetCommaSeparatedOf(Result.SentBlockCount);
                lblRes_SentEmailCount.Text = UIUtil.GetCommaSeparatedOf(Result.SentEmailCount);
                lblRes_TotalMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.TotalEmailCount);
                lblRes_TotalBlockMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.TotalBlockCount);
                if (Result.InvalidAddressMailCount>0)
                    lblRes_InvalidMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.InvalidAddressMailCount);
            }
            else if (Result.SentEmailCount == 0)
            {
                lblRes_BlockSize.Text = UIUtil.GetCommaSeparatedOf(Result.BlockSize);
                lblRes_FailedBlockCount.Text = UIUtil.GetCommaSeparatedOf(Result.FailedBlockCount);
                lblRes_FailedEmailCount.Text = UIUtil.GetCommaSeparatedOf(Result.FailedEmailCount);
                lblRes_TotalMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.TotalEmailCount);
                
                if (Result.InvalidAddressMailCount > 0)
                    lblRes_InvalidMailCount.Text = UIUtil.GetCommaSeparatedOf(Result.InvalidAddressMailCount);
            }
            
            //long ChargeAmount = Convert.ToInt64(Result.ChargeAmount);
            //ChargeAmount = (ChargeAmount < 0) ? -1 * ChargeAmount : ChargeAmount;
            //lblRes_ChargeAmount.Text = UIUtil.GetCommaSeparatedOf(ChargeAmount) + " ریال";
            //lblRes_CurrentBalance.Text = UIUtil.GetCommaSeparatedOf(Convert.ToInt64(Result.CurrentBalance)) + " ریال";
        }
    }
}