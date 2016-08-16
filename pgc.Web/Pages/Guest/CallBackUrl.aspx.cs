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


       
            Response.Redirect(GetRouteUrl("guest-orderdetail", new { id = Payment.Order_ID }));
        

        //if (!IsPostBack)
        //{
        //    bool isDone = true;
            
        //    try
        //    {
        //        if (RequestUnpack())
        //        {
        //            SamanOnlinePayment Saman=new SamanOnlinePayment();

        //            //Step 2 Check For Double Spending & Recognize The State
        //            OperationResult opCheckDoubleSpending = Saman.CheckDoubleSpending(transactionState, refrenceNumber, reservationNumber);
        //            if (opCheckDoubleSpending.Result != ActionResult.Done)
        //            {
        //                foreach (var msg in opCheckDoubleSpending.Messages)
        //                    UserSession.AddMessage(msg);
        //                isDone = false;
        //            }

        //            if (opCheckDoubleSpending.Result == ActionResult.Done)
        //            {
        //                //Step 3 If State & Result Are OK Then Verify To Bank
        //                OperationResult opVerify = Saman.VerifyTransaction(refrenceNumber);
        //                foreach (var msg in opVerify.Messages)
        //                    UserSession.AddMessage(msg);

        //                if (opVerify.Result != ActionResult.Done)
        //                    isDone = false;
        //            }                    
        //        }
        //        else
        //            throw new Exception();
        //    }
        //    catch (Exception ex)
        //    {
        //        UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.FailedOnlineTransaction);
        //        Response.Redirect(GetRouteUrl("guest-orderlist", null));
        //        return;
        //    }

        //    OnlinePayment online = new OnlinePaymentBusiness().RetrieveByRefNum(refrenceNumber);
        //    if (online != null)
        //    {
        //        if (isDone)
        //        {
        //            if (UserSession.CurrentMessages.Count == 0)
        //            {
        //                UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.Succeed);
        //            }
        //            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlinePaymentSucceedText);
        //            Response.Redirect(GetRouteUrl("guest-orderdetail", new { id = online.Order_ID }));
        //        }
        //        else
        //            Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + online.ID);
        //    }
        //    else
        //        Response.Redirect(GetRouteUrl("guest-orderlist", null));
        //}
    }

    private OperationResult HandleResult()
    {
        //if (string.IsNullOrEmpty(Request.QueryString["getway"]))
        //    Response.Redirect(GetRouteUrl("guest-default", null));

        //OnlineGetway getway = (OnlineGetway)Enum.Parse(typeof(OnlineGetway), Request.QueryString["getway"], true);

        OperationResult HandleResult = new OperationResult();

        //switch (getway)
        //{
        //    case OnlineGetway.SamanBankGateWay:
        //        string RefNum = (string.IsNullOrEmpty(Request.Form["RefNum"])) ? "" : Request.Form["RefNum"];
        //        long ResNum = (string.IsNullOrEmpty(Request.Form["ResNum"])) ? 0 : ConvertorUtil.ToInt64(Request.Form["ResNum"]);
        //        SamanOnlinePaymentState State = (string.IsNullOrEmpty(Request.Form["State"])) ? SamanOnlinePaymentState.NoReturnFromBank : EnumUtil.ParseEnum<SamanOnlinePaymentState>(Request.Form["State"].Replace(' ', '_'));
        //        HandleResult = new SamanOnlinePaymentBusiness().HandleReturnRequest(ResNum, RefNum, State);
        //        break;

        //    case OnlineGetway.MellatBankGateWay:
                string SaleReferenceId = (string.IsNullOrEmpty(Request.Form["SaleReferenceId"])) ? string.Empty : Request.Form["SaleReferenceId"];
                long saleOrderId = (string.IsNullOrEmpty(Request.Form["saleOrderId"])) ? 0 : ConvertorUtil.ToInt64(Request.Form["saleOrderId"]);
                MellatOnlinePaymentState ResCode = (string.IsNullOrEmpty(Request.Form["ResCode"])) ? MellatOnlinePaymentState.NoReturnFromBank : (MellatOnlinePaymentState)(ConvertorUtil.ToInt32(Request.Form["ResCode"]));
                HandleResult = new MellatOnlinePaymentBusiness().HandleReturnRequest(saleOrderId, SaleReferenceId, ResCode);
        //        break;

        //    case OnlineGetway.ParsianBankGateWay:
        //        string authority = Request.Params["au"] ?? "";
        //        long orderId = (string.IsNullOrEmpty(Request.QueryString["payid"])) ? 0 : ConvertorUtil.ToInt64(Request.QueryString["payid"]);
        //        ParsianOnlinePaymentState response = (string.IsNullOrEmpty(Request.Params["rs"])) ? ParsianOnlinePaymentState.NoReturnFromBank : (ParsianOnlinePaymentState)(ConvertorUtil.ToInt32(Request.Params["rs"]));
        //        HandleResult = new ParsianOnlinePaymentBusiness().HandleReturnRequest(orderId, authority, response);
        //        break;

        //    default:
        //        break;
        //}
        return HandleResult;
    }

    //private bool RequestUnpack()
    //{
    //    if (RequestFeildIsEmpity()) return false;

    //    refrenceNumber = Request.Form["RefNum"].ToString();
    //    reservationNumber = Request.Form["ResNum"].ToString();
    //    if (Request.Form["State"].Count() > 0)
    //        transactionState = Request.Form["State"].ToString();
    //    else if (Request.Form["state"].Count() > 0)
    //        transactionState = Request.Form["state"].ToString();

    //    return true;
    //}

    //private bool RequestFeildIsEmpity()
    //{
    //    if (Request.Form["RefNum"].ToString().Equals(string.Empty))
    //    {
    //        UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineRefNumIsEmpty);
    //        return true;
    //    }
    //    if (Request.Form["state"].ToString().Equals(string.Empty) && Request.Form["State"].ToString().Equals(string.Empty))
    //    {
    //        UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineTransactionStateIsEmpty);
    //        return true;
    //    }
    //    if (Request.Form["ResNum"].ToString().Equals(string.Empty))
    //    {
    //        UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineResNumIsEmpty);
    //        return true;
    //    }
    //    return false;
    //}
}