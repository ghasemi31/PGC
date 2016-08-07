using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Model;
using pgc.Business.Core;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business;

public partial class Pages_Agent_BranchPayWizard_Default : BasePage
{
    public BranchPayment payment = new BranchPayment();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.User.Branch_ID.HasValue)
            Response.Redirect(GetRouteUrl("agent-default", null));
    }
    protected void online1click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 1;
    }
    protected void offline1click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 2;
    }

    #region online btns
    protected void online1go_Click(object sender, EventArgs e)
    {

        string ResNum = new SamanOnlinePayment().BranchCreateReservationNumber(UserSession.User.Branch_ID.Value, nmrPriceOnline.GetNumber<long>());

        //if (!string.IsNullOrEmpty(ResNum))
        //{
        //    #region Order_Online_New

        //    hoosh.Business.SystemEventArgs eArgs = new hoosh.Business.SystemEventArgs();

        //    eArgs.Related_Guest_Email = order.Email;
        //    eArgs.Related_Guest_Phone = order.Mobile;

        //    eArgs.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));

        //    eArgs.EventVariables.Add("%email%", order.Email);
        //    eArgs.EventVariables.Add("%mobile%", order.Mobile);
        //    eArgs.EventVariables.Add("%address%", order.Address);
        //    eArgs.EventVariables.Add("%description%", order.Description);
        //    eArgs.EventVariables.Add("%name%", order.Name);
        //    eArgs.EventVariables.Add("%orderedtitle%", order.OrderedTitle);
        //    eArgs.EventVariables.Add("%ordertype%", EnumUtil.GetEnumElementPersianTitle((OrderType)order.OrderType));
        //    eArgs.EventVariables.Add("%postalcode%", order.PostalCode);
        //    eArgs.EventVariables.Add("%quantity%", order.Quantity.ToString());
        //    eArgs.EventVariables.Add("%singlecost%", UIUtil.GetCommaSeparatedOf(order.SingleCost) + " ریال");
        //    eArgs.EventVariables.Add("%totalcost%", UIUtil.GetCommaSeparatedOf(order.TotalCost) + " ریال");

        //    hoosh.Business.EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Online_New, eArgs);

        //    #endregion

        Response.Redirect(GetRouteUrl("agent-gotosaman", new { id = ResNum }));
    }
    protected void online1Back_Click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 0;
    }
    #endregion

    #region Offline BTNS
    protected void btnofflineBack_Click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 0;
    }
    protected void btnSaveOffline_Click(object sender, EventArgs e)
    {
        if (lkpBankAccount.GetSelectedValue<long>() <= 0)
        {
            UserSession.AddMessage(UserMessageKey.PleaseSelectBankAccount);
            return;
        }

        BranchPayment off = new BranchPayment();

        off.Amount = nmrOfflinePrice.GetNumber<long>();        
        off.OfflineBankAccount_ID = lkpBankAccount.GetSelectedValue<long>();
        BankAccount bankAccount = new pgc.Business.BankAccountBusiness().Retrieve(off.OfflineBankAccount_ID.Value); ;
        off.OfflineBankAccountDescription = bankAccount.Description;
        off.OfflineBankAccountTitle = bankAccount.Title;

        off.Date = DateTime.Now;
        off.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
        off.OfflineDescription = txtOfflineDesc.Text;
        off.OfflinePaymentStatus = (int)BranchOfflinePaymentStatus.Pending;

        off.OfflineReceiptLiquidator = txtReceiptLiquidator.Text;
        off.OfflineReceiptNumber = txtReceiptNumber.Text;
        off.OfflineReceiptPersianDate = pdpReceiptPersianDate.PersianDate;
        off.OfflineReceiptType = (int)lkpRecieptType.GetSelectedValue<BranchOfflineReceiptType>();

        off.Type = (int)BranchPaymentType.Offline;

        off.Branch_ID = UserSession.User.Branch_ID.Value;

        OperationResult op = new BranchPaymentBusiness().Insert(off);
        if (op.Result == ActionResult.Done)
        {
            UserSession.AddMessage(UserMessageKey.BranchOfflineInsertSucceed);

            //UserSession.AddMessage(UserMessageKey.ThanksForOfflineOrdering);


            //#region Order_Offline_New

            //hoosh.Business.SystemEventArgs eArgs = new hoosh.Business.SystemEventArgs();

            //eArgs.Related_Guest_Email = order.Email;
            //eArgs.Related_Guest_Phone = order.Mobile;

            //eArgs.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));

            //eArgs.EventVariables.Add("%email%", order.Email);
            //eArgs.EventVariables.Add("%mobile%", order.Mobile);
            //eArgs.EventVariables.Add("%address%", order.Address);
            //eArgs.EventVariables.Add("%description%", off.Description);
            //eArgs.EventVariables.Add("%name%", order.Name);
            //eArgs.EventVariables.Add("%orderedtitle%", order.OrderedTitle);
            //eArgs.EventVariables.Add("%ordertype%", EnumUtil.GetEnumElementPersianTitle((OrderType)order.OrderType));
            //eArgs.EventVariables.Add("%postalcode%", order.PostalCode);
            //eArgs.EventVariables.Add("%quantity%", order.Quantity.ToString());
            //eArgs.EventVariables.Add("%singlecost%", UIUtil.GetCommaSeparatedOf(order.SingleCost) + " ریال");
            //eArgs.EventVariables.Add("%totalcost%", UIUtil.GetCommaSeparatedOf(order.TotalCost) + " ریال");

            //eArgs.EventVariables.Add("%bankdesc%", off.BankAccountDescription);
            //eArgs.EventVariables.Add("%banktitle%", off.BankAccountTitle);
            //eArgs.EventVariables.Add("%liquidator%", off.ReceiptLiquidator);
            //eArgs.EventVariables.Add("%rnum%", off.ReceiptNumber);
            //eArgs.EventVariables.Add("%rdate%", off.ReceiptPersianDate);
            //eArgs.EventVariables.Add("%rtype%", EnumUtil.GetEnumElementPersianTitle((OfflineReceiptType)off.ReceiptType));

            //hoosh.Business.EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Offline_New, eArgs);

            //#endregion

            Response.Redirect(GetRouteUrl("agent-branchpayment", null));
        }
        else
            foreach (var item in op.Messages)
            {
                UserSession.AddMessage(op.Messages);
            }
    }

    #endregion
}