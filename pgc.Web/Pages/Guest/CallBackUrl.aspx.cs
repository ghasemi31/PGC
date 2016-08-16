using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using pgc.Business.Core;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business;
using kFrameWork.Util;
using pgc.Model.Enums;
using pgc.Business.Payment.OnlinePay;

public partial class Pages_Guest_CallBackUrl : BasePage
{
    private string refrenceNumber = string.Empty;
    private string reservationNumber = string.Empty;
    private string transactionState = string.Empty;

    pgc.Business.Payment.OnlinePay.PaymentBusiness business = new pgc.Business.Payment.OnlinePay.PaymentBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {
        OperationResult Result = HandleResult();

        UserSession.AddMessage(Result.Messages);
        UserSession.AddCompeleteMessage(Result.CompleteMessages);

        Payment Payment = null;

        if (Result.Data.Keys.Contains("OnlinePayment"))
            Payment = (Payment)Result.Data["OnlinePayment"];

        if (Payment == null)
            Response.Redirect(GetRouteUrl("guest-default", null));



        Response.Redirect(GetRouteUrl("guest-ordertracking", new { id = Payment.Order_ID }));
        

    }

    private OperationResult HandleResult()
    {
        if (string.IsNullOrEmpty(Request.QueryString["getway"]))
            Response.Redirect(GetRouteUrl("guest-default", null));

        OnlineGetway getway = (OnlineGetway)Enum.Parse(typeof(OnlineGetway), Request.QueryString["getway"], true);

        OperationResult HandleResult = new OperationResult();

        switch (getway)
        {
            case OnlineGetway.AsanPardakhtGateWay:
                // asan pardakht here
                break;

            case OnlineGetway.MellatBankGateWay:
                string SaleReferenceId = (string.IsNullOrEmpty(Request.Form["SaleReferenceId"])) ? string.Empty : Request.Form["SaleReferenceId"];
                long saleOrderId = (string.IsNullOrEmpty(Request.Form["saleOrderId"])) ? 0 : ConvertorUtil.ToInt64(Request.Form["saleOrderId"]);
                MellatOnlinePaymentState ResCode = (string.IsNullOrEmpty(Request.Form["ResCode"])) ? MellatOnlinePaymentState.NoReturnFromBank : (MellatOnlinePaymentState)(ConvertorUtil.ToInt32(Request.Form["ResCode"]));
                HandleResult = new MellatOnlinePaymentBusiness().HandleReturnRequest(saleOrderId, SaleReferenceId, ResCode);
                break;


            default:
                break;
        }
        return HandleResult;
    }

    
}