using kFrameWork.Business;
using kFrameWork.Model;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business;
using pgc.Business.Payment.OnlinePay;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_OnlinePayment : BasePage
{

    public GameOrder order = null;
    PaymentBusiness pay_business = new PaymentBusiness();
    public string RefId;
    long PayID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.HasValidQueryString<long>(QueryStringKeys.id))
            Response.Redirect(GetRouteUrl("guest-default", null));
        else
        {
            order = pay_business.RetrieveOrder(this.GetQueryStringValue<long>(QueryStringKeys.id));

            if (order.IsPaid)
                Response.Redirect(GetRouteUrl("guest-default", null));

            OperationResult res = new OperationResult();
            res = new pgc.Business.Payment.OnlinePay.PaymentBusiness().CreatePayment(order.ID, OnlineGetway.MellatBankGateWay);

            if (res.Result == ActionResult.Done)
            {
                PayID = (long)res.Data["ResNum"];
                Post();
            }
        }
    }


    private void Post()
    {

        try
        {
            string result;
            string redirectUrl = ConfigurationManager.AppSettings["WebSiteURL"] + "/Pages/Guest/CallBackUrl.aspx";
            long terminalId = ConvertorUtil.ToInt64(OptionBusiness.GetText(pgc.Model.Enums.OptionKey.Mellat_TerminalId));
            BypassCertificateError();


            pgc.Business.ir.shaparak.bpm.PaymentGatewayImplService bpService = new pgc.Business.ir.shaparak.bpm.PaymentGatewayImplService();
            result = bpService.bpPayRequest(terminalId,
                OptionBusiness.GetText(pgc.Model.Enums.OptionKey.Mellat_UserName),
                OptionBusiness.GetText(pgc.Model.Enums.OptionKey.Mellat_Password),
                PayID,
                (order.PayableAmount),
                DateTime.Now.ToString("yyyyMMdd"),
                DateTime.Now.ToString("HHmmss"),
                "",
                redirectUrl,
               0);

            String[] resultArray = result.Split(',');

            int State = ConvertorUtil.ToInt32(resultArray[0]);

            if (State == (int)MellatOnlinePaymentState.OK)
                RefId = resultArray[1];
            else
            {
                string message = EnumUtil.GetEnumElementPersianTitle((MellatOnlinePaymentState)State);
                UserSession.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));

                pay_business.ChangeOrderState(PayID, State);


                Response.Redirect(GetRouteUrl("user-gamedetail", new { id = order.ID }));

            }
        }
        catch (Exception exp)
        {
            UserSession.AddMessage(UserMessageKey.BankWebServiceNoResponse);

        }
    }

    void BypassCertificateError()
    {
        ServicePointManager.ServerCertificateValidationCallback +=
            delegate(
                Object sender1,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
    }
}