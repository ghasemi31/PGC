using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model;
using kFrameWork.Model;
using System.Web;
using kFrameWork.Util;
using pgc.Model.Enums;
using kFrameWork.Business;
using pgc.Business.ir.shaparak.sep;
using pgc.Model.Other.Project;
using kFrameWork.UI;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace pgc.Business.Payment.OnlinePay
{
    public class MellatOnlinePaymentBusiness : OnlinePaymentBusiness
    {


        /// <summary>
        /// Handle return posted request from bank , check , verify  and ...
        /// </summary>
        /// <param name="SaleReferenceId"> رسید دیجیتالی</param>
        /// <param name="saleOrderId"> شناسه رکورد جدول پرداخت آنلاین</param>
        /// <param name="State">Enumration وضعیت تراکنش بازگشتی از بانک</param>
        /// <returns></returns>
        public OperationResult HandleReturnRequest(long saleOrderId, string SaleReferenceId, MellatOnlinePaymentState State)
        {
            OperationResult res = new OperationResult();
            try
            {
                pgc.Model.Payment Payment = Context.Payments.SingleOrDefault(p => p.ID == saleOrderId);

                //recived res num does is fake or related to a deleted order
                if (Payment == null)
                {
                    res.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                Payment.BankGeway_Enum = (int)OnlineGetway.MellatBankGateWay;

                //payment transaction did not sucees
                if (State != MellatOnlinePaymentState.OK)
                {
                    //Persist Payment Changes
                    Payment.RefNum = SaleReferenceId;
                    Payment.State = (int)State;
                    Context.SaveChanges();

                    string message = EnumUtil.GetEnumElementPersianTitle(State);
                    //res.AddMessage(EnumUtil.ParseEnum<UserMessageKey>(State.ToString().Replace(' ', '_')));
                    res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));
                    res.Result = ActionResult.Failed;
                    res.Data.Add("OnlinePayment", Payment);
                    return res;
                }

                // check the case state is ok but RefNum is empty (actually this case wont happen)
                if (SaleReferenceId == "")
                {
                    //Persist Payment Changes
                    Payment.RefNum = SaleReferenceId;
                    Payment.State = (int)MellatOnlinePaymentState.Sale_NotFound;
                    Context.SaveChanges();

                    res.AddMessage(UserMessageKey.NoRefNum);
                    res.Result = ActionResult.Failed;
                    res.Data.Add("OnlinePayment", Payment);
                    return res;
                }

                //ref num exist in our db .. means sbody try to Double Spending or some mistake is happening ...
                if (Context.Payments.Any(p => p.RefNum == SaleReferenceId))
                {
                    res.AddMessage(UserMessageKey.DoubleSpending);
                    res.Result = ActionResult.Failed;
                    res.Data.Add("OnlinePayment", Payment);
                    return res;
                }

                Payment.RefNum = SaleReferenceId;
                Payment.State = (int)State;
                Context.SaveChanges();

                return Verify(saleOrderId, SaleReferenceId, true);
            }
            catch
            {
                res.AddMessage(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
            }
        }

        public OperationResult Verify(long saleOrderId, string SaleReferenceId, bool CallFromHandleReturnRequest)
        {
            OperationResult res = new OperationResult();
            try
            {
                pgc.Model.Payment Payment = Context.Payments.SingleOrDefault(p => p.ID == saleOrderId);

                //recived res num is fake or related to a deleted order
                if (Payment == null)
                {
                    res.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                if (string.IsNullOrEmpty(SaleReferenceId))
                {
                    res.AddMessage(UserMessageKey.NoRefNum);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                long TerminalID = ConvertorUtil.ToInt64(OptionBusiness.GetText(OptionKey.Mellat_TerminalId));
                string userName = OptionBusiness.GetText(OptionKey.Mellat_UserName);
                string pass = OptionBusiness.GetText(OptionKey.Mellat_Password);

                ir.shaparak.bpm.PaymentGatewayImplService bpService = new ir.shaparak.bpm.PaymentGatewayImplService();

                if (CallFromHandleReturnRequest)
                {
                    BypassCertificateError();

                    string ver_res = bpService.bpVerifyRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId));

                    //unknown verifay result, Inquiry again
                    if (string.IsNullOrEmpty(ver_res))
                    {
                        BypassCertificateError();
                        ver_res = bpService.bpInquiryRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId));

                    }

                    int ver_result = (!string.IsNullOrEmpty(ver_res)) ? ConvertorUtil.ToInt32(ver_res) : (int)MellatOnlinePaymentState.Verify_NotFound;

                    //verifay result is success
                    if (ver_result == (int)MellatOnlinePaymentState.OK)
                    {
                        //BypassCertificateError();
                        //int settle_res = ConvertorUtil.ToInt32(bpService.bpSettleRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId)));

                        // PAYMENT SUCEEDD
                        Payment.GameOrder.IsPaid = true;
                        Payment.Amount = Payment.GameOrder.PayableAmount;
                        Context.SaveChanges();

                        res.Messages.Add(UserMessageKey.OnlinePaymentIsSuccessful);


                        //#region Raise System Event 'Online_Payment_Succeed' 'ByUser'
                        //SystemEventArgs sea = new SystemEventArgs();
                        //sea.GameOrderTransfree_Email = Payment.Order.Transfree_Email;
                        //sea.OrderTransfree_Phone = Payment.Order.Transfree_Mobile;
                        //sea.EventVariables.Add("%date%", Payment.PersianDate);
                        //sea.EventVariables.Add("%time%", Payment.Date.ToShortTimeString());
                        //if (Payment.Order.User != null)
                        //{
                        //    sea.Customer = Payment.Order.User;
                        //    sea.EventVariables.Add("%customer_name%", Util.GetFullName(Payment.Order.User));
                        //    sea.EventVariables.Add("%customer_name_gender%", Util.GetFullNameWithGender(Payment.Order.User));
                        //    sea.EventVariables.Add("%username%", Payment.Order.User.Username);
                        //}
                        //else
                        //{
                        //    sea.Guest_Email = Payment.Order.Guest.Email;
                        //    sea.EventVariables.Add("%username%", Payment.Order.Guest.Email);
                        //}
                        //sea.EventVariables.Add("%order_code%", Payment.Order.Code);
                        //sea.EventVariables.Add("%order_date%", Payment.Order.PersianDate);
                        //sea.EventVariables.Add("%order_time%", DateUtil.GetPersianDateShortString(Payment.Order.Date));
                        //sea.EventVariables.Add("%order_amount%", UIUtil.GetCommaSeparatedOf(Payment.Order.PayableAmount) + " تومان");
                        //sea.EventVariables.Add("%payment_amount%", UIUtil.GetCommaSeparatedOf(Payment.Amount) + " تومان");
                        //sea.EventVariables.Add("%ref_num%", Payment.RefNum);
                        //sea.EventVariables.Add("%payment_state%", EnumUtil.GetEnumElementPersianTitle(Payment.State));
                        //sea.EventVariables.Add("%order_payment_status%", EnumUtil.GetEnumElementPersianTitle((PaymentStatus)Payment.Order.PaymentStatus));
                        //sea.EventVariables.Add("%ip%", Payment.IPAddress);

                        //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Online_Payment_Succeed, sea);
                        //#endregion

                        res.Messages.Add(UserMessageKey.Succeed);
                        res.Result = ActionResult.Done;
                        res.Data.Add("OnlinePayment", Payment);
                        return res;
                    }

                    //verifay result is faild
                    else
                    {
                        MellatOnlinePaymentState ver_state = (MellatOnlinePaymentState)(ver_result);

                        //Persist Payment Changes
                        Payment.RefNum = SaleReferenceId;
                        Payment.State = (int)ver_state;
                        Context.SaveChanges();

                        string message = EnumUtil.GetEnumElementPersianTitle(ver_state);
                        res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));
                        res.Result = ActionResult.Failed;
                        res.Data.Add("OnlinePayment", Payment);
                        return res;
                    }
                }
                // this means if verify is calling from admin
                else if (Payment.State != (int)MellatOnlinePaymentState.OK)
                {
                    BypassCertificateError();
                    string ver_inq = bpService.bpInquiryRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId));
                    int ver_result = (!string.IsNullOrEmpty(ver_inq)) ? ConvertorUtil.ToInt32(ver_inq) : (int)MellatOnlinePaymentState.Verify_NotFound;

                    //verifay result is success
                    if (ver_result == (int)MellatOnlinePaymentState.OK)
                    {
                        // PAYMENT SUCEEDD
                        Payment.GameOrder.IsPaid = true;
                        Payment.Amount = Payment.GameOrder.PayableAmount;
                        Context.SaveChanges();

                        //#region Raise System Event 'Qualify_Online_Payment_Succeed' 'ByUser'
                        //SystemEventArgs sea = new SystemEventArgs();
                        //sea.Customer = Payment.Order.User;
                        //sea.OrderTransfree_Email = Payment.Order.Transfree_Email;
                        //sea.OrderTransfree_Phone = Payment.Order.Transfree_Mobile;

                        //sea.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                        //sea.EventVariables.Add("%time%", DateTime.Now.ToShortTimeString());
                        //sea.EventVariables.Add("%payment_date%", Payment.PersianDate);
                        //sea.EventVariables.Add("%payment_time%", Payment.Date.ToShortTimeString());
                        //sea.EventVariables.Add("%customer_name%", Util.GetFullName(Payment.Order.User));
                        //sea.EventVariables.Add("%customer_name_gender%", Util.GetFullNameWithGender(Payment.Order.User));
                        //sea.EventVariables.Add("%username%", Payment.Order.User.Username);

                        //sea.EventVariables.Add("%order_code%", Payment.Order.Code);
                        //sea.EventVariables.Add("%order_date%", Payment.Order.PersianDate);
                        //sea.EventVariables.Add("%order_time%", Payment.Order.Date.ToShortTimeString());
                        //sea.EventVariables.Add("%order_amount%", UIUtil.GetCommaSeparatedOf(Payment.Order.PayableAmount) + " تومان");

                        //sea.EventVariables.Add("%payment_amount%", UIUtil.GetCommaSeparatedOf(Payment.Amount) + " تومان");
                        //sea.EventVariables.Add("%ref_num%", Payment.RefNum);
                        //sea.EventVariables.Add("%payment_state%", EnumUtil.GetEnumElementPersianTitle((SamanOnlinePaymentState)Payment.State));
                        //sea.EventVariables.Add("%order_payment_status%", EnumUtil.GetEnumElementPersianTitle((PaymentStatus)Payment.Order.PaymentStatus));
                        //sea.EventVariables.Add("%ip%", Payment.IPAddress);

                        //sea.EventVariables.Add("%admin_name%", UserSession.User.Fname + " " + UserSession.User.Lname);
                        //sea.EventVariables.Add("%admin_username%", UserSession.User.Username);

                        //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Qualify_Online_Payment_Succeed, sea);
                        //#endregion

                        res.Messages.Add(UserMessageKey.Succeed);
                        res.Messages.Add(UserMessageKey.OnlinePaymentIsSuccessful);
                        res.Result = ActionResult.Done;
                        res.Data.Add("OnlinePayment", Payment);
                        return res;
                    }

                    //verifay result is faild
                    else
                    {
                        MellatOnlinePaymentState ver_state = (MellatOnlinePaymentState)(ver_result);

                        //Persist Payment Changes
                        Payment.RefNum = SaleReferenceId;
                        Payment.State = (int)ver_state;
                        Context.SaveChanges();

                        string message = EnumUtil.GetEnumElementPersianTitle(ver_state);
                        res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));
                        res.Result = ActionResult.Failed;
                        res.Data.Add("OnlinePayment", Payment);
                        return res;
                    }
                }
                else
                {
                    res.Messages.Add(UserMessageKey.OnlinePaymentIsSuccessful);
                    res.Result = ActionResult.Done;
                    res.Data.Add("OnlinePayment", Payment);
                    return res;
                }
            }
            catch
            {
                res.Messages.Add(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
            }
        }

        public OperationResult Settle(long saleOrderId, string SaleReferenceId)
        {
            OperationResult res = new OperationResult();
            try
            {
                pgc.Model.Payment Payment = Context.Payments.SingleOrDefault(p => p.ID == saleOrderId);

                if (Payment == null)
                {
                    res.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                if (string.IsNullOrEmpty(SaleReferenceId))
                {
                    res.AddMessage(UserMessageKey.NoRefNum);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                long TerminalID = ConvertorUtil.ToInt64(OptionBusiness.GetText(OptionKey.Mellat_TerminalId));
                string userName = OptionBusiness.GetText(OptionKey.Mellat_UserName);
                string pass = OptionBusiness.GetText(OptionKey.Mellat_Password);

                ir.shaparak.bpm.PaymentGatewayImplService bpService = new ir.shaparak.bpm.PaymentGatewayImplService();
                BypassCertificateError();
                string settle_res = bpService.bpSettleRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId));
                int settle_result = (!string.IsNullOrEmpty(settle_res)) ? ConvertorUtil.ToInt32(settle_res) : (int)MellatOnlinePaymentState.Verify_NotFound;
                MellatOnlinePaymentState settle_state = (MellatOnlinePaymentState)(settle_result);
                string message = EnumUtil.GetEnumElementPersianTitle(settle_state);
                res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));
                res.Result = ActionResult.Done;
                res.Data.Add("OnlinePayment", Payment);
                return res;

            }
            catch
            {
                res.Messages.Add(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
            }
        }
        /// <summary>
        /// RoleBack an onlinepayment , be care full that transaction should have refnum and ok state , and posotive amount
        /// </summary>
        /// <param name="saleOrderId"> شناسه رکورد جدول پرداخت آنلاین</param>
        /// <param name="SaleReferenceId">رسید دیجیتالی</param>
        /// <returns></returns>
        public OperationResult Reverse(long saleOrderId, string SaleReferenceId)
        {
            OperationResult res = new OperationResult();
            try
            {
                pgc.Model.Payment Payment = Context.Payments.SingleOrDefault(p => p.ID == saleOrderId);

                //recived res num is fake or related to a deleted order
                if (Payment == null)
                {
                    res.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    res.Result = ActionResult.Failed;
                    return res;
                }
                if (string.IsNullOrEmpty(SaleReferenceId))
                {
                    res.AddMessage(UserMessageKey.NoRefNum);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                long TerminalID = ConvertorUtil.ToInt64(OptionBusiness.GetText(OptionKey.Mellat_TerminalId));
                string userName = OptionBusiness.GetText(OptionKey.Mellat_UserName);
                string pass = OptionBusiness.GetText(OptionKey.Mellat_Password);

                ir.shaparak.bpm.PaymentGatewayImplService bpService = new ir.shaparak.bpm.PaymentGatewayImplService();


                BypassCertificateError();

                string rev_res = bpService.bpReversalRequest(TerminalID, userName, pass, saleOrderId, saleOrderId, ConvertorUtil.ToInt64(SaleReferenceId));

                if (string.IsNullOrEmpty(rev_res))
                {
                    //could not connect , bank did not answer ,web service problem or other prblems....
                    res.AddMessage(UserMessageKey.Failed);
                    res.Result = ActionResult.Failed;
                    return res;
                }

                int rev_result = ConvertorUtil.ToInt32(rev_res);

                if (rev_result == (int)MellatOnlinePaymentState.OK)
                {
                    //succeed
                    pgc.Model.Payment newOp = new pgc.Model.Payment();
                    newOp.Amount = -1 * Payment.Amount;
                    newOp.Date = DateTime.Now;
                    newOp.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    newOp.Order_ID = Payment.Order_ID;
                    newOp.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                    newOp.RefNum = Payment.RefNum;
                    newOp.State = (int)MellatOnlinePaymentState.ReverseDone;
                    Context.Payments.AddObject(newOp);
                    Context.SaveChanges();
                    res.AddMessage(UserMessageKey.ReverseCompleted);

                    // may need to raise onlinepayment_reverse event
                    //#region Raise System Event
                    //SystemEventArgs sea = new SystemEventArgs();
                    //sea.Customer = Payment.Order.User;
                    //sea.OrderTransfree_Email = Payment.Order.Transfree_Email;
                    //sea.OrderTransfree_Phone = Payment.Order.Transfree_Mobile;

                    //sea.EventVariables.Add("%date%", newOp.PersianDate);
                    //sea.EventVariables.Add("%time%", newOp.Date.ToShortTimeString());
                    //sea.EventVariables.Add("%payment_date%", Payment.PersianDate);
                    //sea.EventVariables.Add("%payment_time%", Payment.Date.ToShortTimeString());
                    //sea.EventVariables.Add("%order_date%", Payment.Order.PersianDate);
                    //sea.EventVariables.Add("%order_time%", Payment.Order.Date.ToShortTimeString());

                    //sea.EventVariables.Add("%customer_name%", Util.GetFullName(Payment.Order.User));
                    //sea.EventVariables.Add("%customer_name_gender%", Util.GetFullNameWithGender(Payment.Order.User));
                    //sea.EventVariables.Add("%username%", Payment.Order.User.Username);

                    //sea.EventVariables.Add("%order_code%", Payment.Order.Code);
                    //sea.EventVariables.Add("%order_amount%", UIUtil.GetCommaSeparatedOf(Payment.Order.PayableAmount) + " تومان");

                    //sea.EventVariables.Add("%payment_amount%", UIUtil.GetCommaSeparatedOf(Payment.Amount) + " تومان");
                    //sea.EventVariables.Add("%ref_num%", Payment.RefNum);
                    //sea.EventVariables.Add("%payment_state%", EnumUtil.GetEnumElementPersianTitle(newOp.State));
                    //sea.EventVariables.Add("%order_payment_status%", EnumUtil.GetEnumElementPersianTitle((PaymentStatus)Payment.Order.PaymentStatus));

                    //sea.EventVariables.Add("%admin_name%", UserSession.User.Fname + " " + UserSession.User.Lname);
                    //sea.EventVariables.Add("%admin_username%", UserSession.User.Username);

                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Online_Payment_Reverse, sea);
                    //#endregion

                    if (Payment.Amount == Payment.GameOrder.PayableAmount && Payment.GameOrder.IsPaid)
                    {
                        Payment.GameOrder.IsPaid = false;
                        Context.SaveChanges();
                        res.AddMessage(UserMessageKey.PaymentStatusChangedToNotPaid);
                        // may need to raise paymentstatus_change event
                    }
                    res.Result = ActionResult.Done;
                }
                else
                {
                    //not succeed
                    MellatOnlinePaymentState rev_state = (MellatOnlinePaymentState)(rev_result);

                    string message = EnumUtil.GetEnumElementPersianTitle(rev_state);
                    res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, message));
                    res.Result = ActionResult.Failed;
                }

                return res;
            }
            catch
            {
                res.AddMessage(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
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
}
