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
using pgc.Model.Enums;

public partial class Pages_Agent_BranchPayWizard_CallBackUrl : System.Web.UI.Page
{
    private string refrenceNumber = string.Empty;
    private string reservationNumber = string.Empty;
    private string transactionState = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool isDone = true;
            
            try
            {
                if (RequestUnpack())
                {
                    SamanOnlinePayment Saman=new SamanOnlinePayment();

                    //Step 2 Check For Double Spending & Recognize The State
                    OperationResult opCheckDoubleSpending = Saman.BranchCheckDoubleSpending(transactionState, refrenceNumber, reservationNumber);
                    if (opCheckDoubleSpending.Result != ActionResult.Done)
                    {
                        foreach (var msg in opCheckDoubleSpending.Messages)
                            UserSession.AddMessage(msg);
                        isDone = false;
                    }

                    if (opCheckDoubleSpending.Result == ActionResult.Done)
                    {
                        //Step 3 If State & Result Are OK Then Verify To Bank
                        OperationResult opVerify = Saman.BranchVerifyTransaction(refrenceNumber);
                        foreach (var msg in opVerify.Messages)
                            UserSession.AddMessage(msg);

                        if (opVerify.Result != ActionResult.Done)
                            isDone = false;
                    }                    
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                UserSession.AddMessage(UserMessageKey.FailedOnlineTransaction);
                Response.Redirect(GetRouteUrl("agent-branchpaywizard", null));
                return;
            }

            BranchPayment online = new BranchPaymentBusiness().RetrieveByRefNum(refrenceNumber);
            if (online != null)
            {
                if (isDone)
                {
                    if (UserSession.CurrentMessages.Count == 0)
                    {
                        UserSession.AddMessage(UserMessageKey.Succeed);
                    }
                    //UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlinePaymentSucceedText);
                    Response.Redirect(GetRouteUrl("agent-branchpayment", null));
                }
                else
                    Response.Redirect(GetRouteUrl("agent-branchpaywizard", null));
            }
            else
                Response.Redirect(GetRouteUrl("agent-branchpaywizard", null));
        }
    }

    private bool RequestUnpack()
    {
        if (RequestFeildIsEmpity()) return false;

        refrenceNumber = Request.Form["RefNum"].ToString();
        reservationNumber = Request.Form["ResNum"].ToString();
        if (Request.Form["State"].Count() > 0)
            transactionState = Request.Form["State"].ToString();
        else if (Request.Form["state"].Count() > 0)
            transactionState = Request.Form["state"].ToString();

        return true;
    }

    private bool RequestFeildIsEmpity()
    {
        if (Request.Form["RefNum"].ToString().Equals(string.Empty))
        {
            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineRefNumIsEmpty);
            return true;
        }
        if (Request.Form["state"].ToString().Equals(string.Empty) && Request.Form["State"].ToString().Equals(string.Empty))
        {
            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineTransactionStateIsEmpty);
            return true;
        }
        if (Request.Form["ResNum"].ToString().Equals(string.Empty))
        {
            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.OnlineResNumIsEmpty);
            return true;
        }
        return false;
    }
}