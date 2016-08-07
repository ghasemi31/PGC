using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Business.Core;
using System.Text.RegularExpressions;
using pgc.Model;
using kFrameWork.Model;
using pgc.Business;
using pgc.Model.Enums;

public partial class Pages_Admin_TestSendSms_Default : BasePage
{
    

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            #region Basic UI Validation

            if (mscBody.Message.Body.Trim() == string.Empty)
            {
                UserSession.AddMessage(UserMessageKey.NoMessageBody);
                return;
            }
            else if (lkcPrivateNo.GetSelectedValue<long>() == -1)
            {
                UserSession.AddMessage(UserMessageKey.NoPrivateNo);
                return;
            }
     
            List<string> Recipients = new List<string>();

            if (!String.IsNullOrEmpty(ntbRecipients.Text.Trim()))
                Recipients.AddRange(Regex.Replace(ntbRecipients.Text.Trim(), @"^\s*$\n", string.Empty, RegexOptions.Multiline).Trim("\n".ToCharArray()).Split("\n".ToCharArray()).ToList());


            #endregion Basic UI Validation

            SendSMSBusiness Business = new SendSMSBusiness(
                mscBody.Message,
                Recipients,
                this.lkcPrivateNo.GetSelectedValue<long>()
                );

            OperationResult ValidationResult = Business.ValidateForSend();
            if (ValidationResult.Result != ActionResult.Done)
            {
                UserSession.AddMessage(ValidationResult.Messages);
                return;
            }

            SendSMSResult Res = Business.Send(null,EventType.Manual);
            UserSession.AddMessage(Res.UserMessages);
            this.snrResult.Result = Res;
            mlvQS.ActiveViewIndex = 1;
        }
        catch
        {
            UserSession.AddMessage(UserMessageKey.Failed);
            
        }
       // OperationResult result= business.ValidateForSend(
       //        mscBody.Message,
       //        lkpPrivateNo.GetSelectedValue<long>(),
       //        Recipients);

       // UserSession.AddMessage(result.Messages);

       //SendResult Res= business.Send(
       //         mscBody.Message,
       //         lkpPrivateNo.GetSelectedValue<long>(),
       //         Recipients);

       //UserSession.AddMessage(Res.UserMessages);
    }

    protected void cmdNewSend_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Admin/SendMessage/Default.aspx");
    }
    protected void cmdSentMessages_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-sentmessage",null);
    }
    protected void BtnReload_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-default",null);
    }
}