using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using kFrameWork.Business;
using System.Text.RegularExpressions;
using kFrameWork.Util;
using pgc.Business.Magfa;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using kFrameWork.UI;
using pgc.Business.ir.shaparak.sep;

namespace pgc.Business.Core
{
    
    public class SamanOnlinePayment
    {
        #region Constant Properties
        public string MerchantID = OptionBusiness.GetText(OptionKey.MerchantID);
        public string MerchantPassword = OptionBusiness.GetText(OptionKey.MerchantPassword);
        public string RedirectURL = OptionBusiness.GetText(OptionKey.RedirectURLForSaman);
        public string BranchRedirectURL = OptionBusiness.GetText(OptionKey.BranchRedirectURLForSaman);
        //public string SamanUrl = "https://acquirer.samanepay.com/Payment.aspx";
        public string SamanUrl = "https://sep.shaparak.ir/Payment.aspx";
        public string SamanListUrl = "https://acquirer.samanepay.com/merchants/";
        #endregion



        #region Functions For User Order Payment

        //Step 1 To Page That Post To Bank
        public string CreateReservationNumber(long Order_ID)
        {
            try
            {
                pgcEntities context = new pgcEntities();
                Order order = context.Orders.SingleOrDefault(f => f.ID == Order_ID);

                OnlinePayment online = new OnlinePayment();
                online.Amount = order.PayableAmount;
                online.Order_ID = Order_ID;
                online.RefNum = "";
                online.ResNum = "";
                online.ResultTransaction = 0;
                online.TransactionState = OnlineTransactionStatus.OnlineRefNumIsEmpty.ToString();

                online.Date = DateTime.Now;
                online.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                context.AddToOnlinePayments(online);

                context.SaveChanges();

                online.ResNum = online.ID.ToString();
                context.SaveChanges();

                return online.ResNum;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "pgc.Business.Core.SamanOnlinePayment.CreateReservationNumber");
                return "";
            }
        }

      
        //Step 2 Get From Bank
        public OperationResult CheckDoubleSpending(string State, string RefNum, string ResNum)
        {
            OperationResult op = new OperationResult();
            op.Result = ActionResult.Done;
            UserMessageKey key = UserMessageKey.FailedOnlineTransaction;
            pgcEntities context = new pgcEntities();

            try
            {
                OnlinePayment online = context.OnlinePayments.SingleOrDefault(f => f.ResNum == ResNum);
                if (online == null)
                {
                    op.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    op.Result = ActionResult.Error;
                    return op;
                }
                key = (UserMessageKey)Enum.Parse(typeof(UserMessageKey), State.Trim().Replace(" ", "_"));
                online.TransactionState = key.ToString();
                
                context.SaveChanges();

                //if RefNum Is Null Failed from bank
                if (string.IsNullOrEmpty(RefNum))
                {
                    op.AddMessage(key);
                    op.Result = ActionResult.Failed;
                    return op;
                }

                
                //Check Double Spending                
                if (context.OnlinePayments.Any(f => f.RefNum == RefNum))
                {
                    op.AddMessage(UserMessageKey.DoubleSpending);
                    op.Result = ActionResult.Failed;
                    return op;
                }

                online.RefNum = RefNum;
                context.SaveChanges();

                if (key != UserMessageKey.OK)
                {
                    op.AddMessage(key);
                    op.Result = ActionResult.Failed;
                    return op;
                }
                else
                {
                    //OnlinePayment_New
                    #region OnlinePayment_New

                    SystemEventArgs eArg = new SystemEventArgs();
                    
                    if (online.Order.Branch_ID.HasValue)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //Details Of User
                    eArg.EventVariables.Add("%user%", eArg.Related_User.FullName);
                    eArg.EventVariables.Add("%username%", eArg.Related_User.Username);
                    eArg.EventVariables.Add("%email%", eArg.Related_User.Email);
                    eArg.EventVariables.Add("%usermobile%", string.IsNullOrEmpty(eArg.Related_User.Mobile) ? eArg.Related_User.Tel : eArg.Related_User.Mobile);
                    
                    //Details Of Order
                    eArg.EventVariables.Add("%phone%", online.Order.Tel);
                    eArg.EventVariables.Add("%address%", string.IsNullOrEmpty(online.Order.Address) ? eArg.Related_User.Address : online.Order.Address);
                    eArg.EventVariables.Add("%description%", online.Order.Comment);
                    eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    eArg.EventVariables.Add("%refnum%", online.RefNum);
                    eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%branch%", online.Order.Branch_ID.HasValue ? online.Order.Branch.Title : "ندارد");
                    eArg.EventVariables.Add("%orderdate%", DateUtil.GetPersianDateShortString(online.Order.OrderDate));
                    eArg.EventVariables.Add("%ordertime%", online.Order.OrderDate.TimeOfDay.ToString().Substring(0, 8));
                    string productlist = "";

                    foreach (var item in online.Order.OrderDetails)
                    {
                        string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                        productlist += temp;
                    }
                    if (productlist.Length > 1)
                        productlist = productlist.Substring(1);

                    eArg.EventVariables.Add("%productlist%", productlist);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(online.Date));
                    eArg.EventVariables.Add("%time%", online.Date.TimeOfDay.ToString().Substring(0, 8));


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_New, eArg);

                    #endregion
                }
            }
            catch (Exception)
            {
                op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                op.Result = ActionResult.Error;
            }
            return op;
        }

        
        //Step 3 Final Function For Paying
        public OperationResult VerifyTransaction(string RefNum)
        {
            OperationResult op = new OperationResult();
            op.Result = ActionResult.Done;
            try
            {
                ///For Ignore SSL Error
                ServicePointManager.ServerCertificateValidationCallback =
                            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                PaymentIFBinding Saman = new PaymentIFBinding();

                pgcEntities context = new pgcEntities();
                OnlinePayment online = context.OnlinePayments.SingleOrDefault(f => f.RefNum == RefNum);


                long result = 0;

                for (int i = 0; i < OptionBusiness.GetInt(OptionKey.TryNumberForVerify); i++)
                {
                    try
                    {
                        result = (long)Saman.verifyTransaction(RefNum, MerchantID);
                        //TEMPORARY
                        //result = online.Amount;
                        i = OptionBusiness.GetInt(OptionKey.TryNumberForVerify);
                    }
                    catch (Exception)
                    { }
                }

                if (result == 0)
                {
                    online.Order.IsPaid = false;
                    online.ResultTransaction = result;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                    op.Result = ActionResult.Failed;
                }
                else if (result < 0)
                {
                    online.ResultTransaction = result;
                    online.Order.IsPaid = false;
                    context.SaveChanges();
                    UserMessageKey key = (UserMessageKey)Enum.Parse(typeof(UserMessageKey), string.Format("Err_{0}", Math.Abs(result)));
                    op.AddMessage(key);
                    op.Result = ActionResult.Error;


                    ////OnlinePayment_Verify
                    #region OnlinePayment_Verify

                    //SystemEventArgs eArg = new SystemEventArgs();
                    //User doer = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //if (UserSession.IsUserLogined)
                    //{
                    //    doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
                    //    if (UserSession.UserID != online.Order.User_ID)
                    //        eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);
                    //}

                    //eArg.Related_Doer = doer;

                    //if (online.Order.Branch_ID.HasValue)
                    //    eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    //eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(doer));
                    //eArg.EventVariables.Add("%username%", doer.Username);
                    //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    //eArg.EventVariables.Add("%mobile%", doer.Mobile);
                    //eArg.EventVariables.Add("%email%", doer.Email);

                    //eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    //eArg.EventVariables.Add("%refnum%", online.RefNum);
                    //eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    //eArg.EventVariables.Add("%state%", UserMessageKeyBusiness.GetUserMessageKey(key));


                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Verify, eArg);

                    #endregion




                    return op;
                }
                else if (result != online.Order.PayableAmount)
                {
                    online.Order.IsPaid = false;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.NeedToReverse);



                    ////OnlinePayment_Verify
                    #region OnlinePayment_Verify

                    //SystemEventArgs eArg = new SystemEventArgs();
                    //User doer = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //if (UserSession.IsUserLogined)
                    //{
                    //    doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
                    //    if (UserSession.UserID != online.Order.User_ID)
                    //        eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);
                    //}

                    //eArg.Related_Doer = doer;

                    //if (online.Order.Branch_ID.HasValue)
                    //    eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    //eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(doer));
                    //eArg.EventVariables.Add("%username%", doer.Username);
                    //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    //eArg.EventVariables.Add("%mobile%", doer.Mobile);
                    //eArg.EventVariables.Add("%email%", doer.Email);

                    //eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    //eArg.EventVariables.Add("%refnum%", online.RefNum);
                    //eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    //eArg.EventVariables.Add("%state%", UserMessageKeyBusiness.GetUserMessageKey(UserMessageKey.NeedToReverse));


                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Verify, eArg);

                    #endregion

                    //OperationResult reverseResult = ReversTransaction(result, RefNum);
                    //if (reverseResult.Result == ActionResult.Done)
                    //    op.Messages.AddRange(reverseResult.Messages);


                    op.Result = ActionResult.Error;
                    return op;
                }
                else if (result == online.Order.PayableAmount)
                {
                    online.ResultTransaction = result;
                    online.Order.IsPaid = true;
                    online.Order.PaymentType = (int)PaymentType.Online;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.OnlineVerificationCompleted);

                    //Finance Cycle Region
                    long oldCredit = (online.Order.Branch_ID.HasValue) ? BranchCreditBusiness.GetBranchCredit(online.Order.Branch_ID.Value) : 0;

                    var transactionInsert=new pgc.Business.BranchTransactionBusiness().InsertCustomerOnlineTransaction(online.ID);
                    if (transactionInsert.Result != ActionResult.Done)
                    {
                        op.AddMessage(UserMessageKey.PleaseNotifyAdminForInsertOnlineTransaction);
                   


                        //Event Raising
                        #region BranchCurrentCredit_Change
                        if (online.Order.Branch_ID.HasValue)
                        {                            

                            SystemEventArgs eArgTransaction = new SystemEventArgs();

                            eArgTransaction.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                            eArgTransaction.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                            eArgTransaction.EventVariables.Add("%branchtitle%", online.Order.BranchTitle);
                            eArgTransaction.EventVariables.Add("%orderid%", online.Order_ID.ToString());
                            eArgTransaction.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");

                            eArgTransaction.EventVariables.Add("%username%", online.Order.User.FullName);
                            eArgTransaction.EventVariables.Add("%email", online.Order.User.Email);

                            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchTransaction_CustomerOnline_Failed, eArgTransaction);
                        }
                        #endregion



                    }




                    //OnlinePayment_Verify
                    #region OnlinePayment_Verify

                    SystemEventArgs eArg = new SystemEventArgs();

                    if (UserSession.IsUserLogined)
                    {
                        User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                        eArg.Related_Doer = doer;

                        eArg.EventVariables.Add("%doer%", doer.FullName);
                        eArg.EventVariables.Add("%doername%", doer.Username);
                    }
                    else
                    {
                        eArg.EventVariables.Add("%doer%", "توسط سیستم انجام شده");
                        eArg.EventVariables.Add("%doername%", "توسط سیستم انجام شده");
                    }

                    if (online.Order.Branch_ID.HasValue)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //Details Of User
                    eArg.EventVariables.Add("%user%",eArg.Related_User.FullName);
                    eArg.EventVariables.Add("%username%", eArg.Related_User.Username);
                    eArg.EventVariables.Add("%email%", eArg.Related_User.Email);
                    eArg.EventVariables.Add("%usermobile%", string.IsNullOrEmpty(eArg.Related_User.Mobile) ? eArg.Related_User.Tel : eArg.Related_User.Mobile);

                    //Details Of Order
                    eArg.EventVariables.Add("%phone%", online.Order.Tel);
                    eArg.EventVariables.Add("%address%", string.IsNullOrEmpty(online.Order.Address) ? eArg.Related_User.Address : online.Order.Address);
                    eArg.EventVariables.Add("%description%", online.Order.Comment);
                    eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    eArg.EventVariables.Add("%refnum%", online.RefNum);
                    eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%branch%", online.Order.Branch_ID.HasValue ? online.Order.Branch.Title : "ندارد");
                    eArg.EventVariables.Add("%orderdate%", DateUtil.GetPersianDateShortString(online.Order.OrderDate));
                    eArg.EventVariables.Add("%ordertime%", online.Order.OrderDate.TimeOfDay.ToString().Substring(0, 8));
                    string productlist = "";

                    foreach (var item in online.Order.OrderDetails)
                    {
                        string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                        productlist += temp;
                    }
                    if (productlist.Length > 1)
                        productlist = productlist.Substring(1);

                    eArg.EventVariables.Add("%productlist%", productlist);

                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(online.Date));
                    eArg.EventVariables.Add("%time%", online.Date.TimeOfDay.ToString().Substring(0, 8));

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Verify, eArg);

                    #endregion
                }

                return op;
            }
            catch (Exception)
            {

                op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                op.Result = ActionResult.Failed;
                return op;
            }
        }

        //If Need To Reverse
        public OperationResult ReversTransaction(long TotalAmount, string RefNum)
        {
            OperationResult op = new OperationResult();
            op.Result = ActionResult.Done;
            try
            {
                pgcEntities context = new pgcEntities();
                OnlinePayment online = context.OnlinePayments.SingleOrDefault(f => f.RefNum == RefNum);
                
                ///For Ignore SSL Error
                ServicePointManager.ServerCertificateValidationCallback =
                            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                PaymentIFBinding Saman = new PaymentIFBinding();
                double result = Saman.reverseTransaction(RefNum, TotalAmount.ToString(), MerchantID, MerchantPassword);
                
                //TEMPORARY
                //double result = online.Amount;
                if (result == 0)
                {
                    online.Order.IsPaid = false;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                    op.Result = ActionResult.Failed;
                }
                else if (result != online.Order.PayableAmount)
                {
                    online.Order.IsPaid = false;                    
                    online.ResultTransaction = (long)result;
                    context.SaveChanges();
                    UserMessageKey key = (UserMessageKey)Enum.Parse(typeof(UserMessageKey), string.Format("Err_{0}", Math.Abs(result)));
                    op.AddMessage(key);
                    op.Result = ActionResult.Error;              
                }
                else if (result == online.Order.PayableAmount)
                {
                    //online.Amount = -20;
                    online.ResultTransaction = -20;
                    online.Order.IsPaid = (online.Amount == online.Order.PayableAmount) ? false : true;
                    context.SaveChanges();

                    op.AddMessage(UserMessageKey.ReverseCompleted);
                    op.Result = ActionResult.Done;


                    //OnlinePayment_Reverse
                    #region OnlinePayment_Reverse

                    SystemEventArgs eArg = new SystemEventArgs();

                    if (UserSession.IsUserLogined)
                    {
                        User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                        eArg.Related_Doer = doer;

                        eArg.EventVariables.Add("%doer%", eArg.Related_Doer.FullName);
                        eArg.EventVariables.Add("%doername%", doer.Username);
                    }
                    else
                    {
                        eArg.EventVariables.Add("%doer%", "مدیر انجام دهنده از سیستم خارج شده");
                        eArg.EventVariables.Add("%doername%", "مدیر انجام دهنده از سیستم خارج شده");
                    }

                    if (online.Order.Branch_ID.HasValue)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //Details Of User
                    eArg.EventVariables.Add("%user%", eArg.Related_User.FullName);
                    eArg.EventVariables.Add("%username%", eArg.Related_User.Username);
                    eArg.EventVariables.Add("%email%", eArg.Related_User.Email);
                    eArg.EventVariables.Add("%usermobile%", string.IsNullOrEmpty(eArg.Related_User.Mobile) ? eArg.Related_User.Tel : eArg.Related_User.Mobile);

                    //Details Of Order
                    eArg.EventVariables.Add("%phone%", online.Order.Tel);
                    eArg.EventVariables.Add("%address%", string.IsNullOrEmpty(online.Order.Address) ? eArg.Related_User.Address : online.Order.Address);
                    eArg.EventVariables.Add("%description%", online.Order.Comment);
                    eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    eArg.EventVariables.Add("%refnum%", online.RefNum);
                    eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%branch%", online.Order.Branch_ID.HasValue ? online.Order.Branch.Title : "ندارد");
                    eArg.EventVariables.Add("%orderdate%", DateUtil.GetPersianDateShortString(online.Order.OrderDate));
                    eArg.EventVariables.Add("%ordertime%", online.Order.OrderDate.TimeOfDay.ToString().Substring(0, 8));
                    string productlist = "";

                    foreach (var item in online.Order.OrderDetails)
                    {
                        string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                        productlist += temp;
                    }
                    if (productlist.Length > 1)
                        productlist = productlist.Substring(1);

                    eArg.EventVariables.Add("%productlist%", productlist);

                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(online.Date));
                    eArg.EventVariables.Add("%time%", online.Date.TimeOfDay.ToString().Substring(0, 8));

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Reverse, eArg);

                    #endregion


                }
            }
            catch (Exception)
            {
                op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                op.Result = ActionResult.Done;
            }
            return op;
        }

        #endregion



        #region Functions For Agent Increase Credit

        //Step 1 To Page That Post To Bank
        public string BranchCreateReservationNumber(long Branch_ID, long Price)
        {
            try
            {
                pgcEntities context = new pgcEntities();

                BranchPayment pay = new BranchPayment();
                pay.Amount = Price;
                pay.Branch_ID = Branch_ID;
                pay.OnlineRefNum = "";
                pay.OnlineResNum = "";
                pay.OnlineResultTransaction = 0;
                pay.OnlineTransactionState = OnlineTransactionStatus.OnlineRefNumIsEmpty.ToString();
                pay.Type = (int)BranchPaymentType.Online;
                pay.Date = DateTime.Now;
                pay.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                context.AddToBranchPayments(pay);

                context.SaveChanges();

                pay.OnlineResNum = "b" + pay.ID.ToString();
                context.SaveChanges();

                return pay.OnlineResNum;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "pgc.Business.Core.SamanOnlinePayment.BranchCreateReservationNumber");
                return "";
            }
        }


        //Step 2 Get From Bank
        public OperationResult BranchCheckDoubleSpending(string State, string RefNum, string ResNum)
        {
            OperationResult op = new OperationResult();
            op.Result = ActionResult.Done;
            UserMessageKey key = UserMessageKey.FailedOnlineTransaction;
            pgcEntities context = new pgcEntities();

            try
            {
                BranchPayment online = context.BranchPayments.SingleOrDefault(f => f.OnlineResNum == ResNum);
                if (online == null)
                {
                    op.AddMessage(UserMessageKey.OnlineResNumIsEmpty);
                    op.Result = ActionResult.Error;
                    return op;
                }
                key = (UserMessageKey)Enum.Parse(typeof(UserMessageKey), State.Trim().Replace(" ", "_"));
                online.OnlineTransactionState = key.ToString();

                context.SaveChanges();

                //if RefNum Is Null Failed from bank
                if (string.IsNullOrEmpty(RefNum))
                {
                    op.AddMessage(key);
                    op.Result = ActionResult.Failed;
                    return op;
                }


                //Check Double Spending                
                if (context.OnlinePayments.Any(f => f.RefNum == RefNum))
                {
                    op.AddMessage(UserMessageKey.DoubleSpending);
                    op.Result = ActionResult.Failed;
                    return op;
                }

                online.OnlineRefNum = RefNum;
                context.SaveChanges();

                if (key != UserMessageKey.OK)
                {
                    op.AddMessage(key);
                    op.Result = ActionResult.Failed;
                    return op;
                }
                else
                {
                    ////OnlinePayment_New
                    //#region OnlinePayment_New

                    //SystemEventArgs eArg = new SystemEventArgs();

                    //eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    //eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    ////Details Of User
                    //eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(eArg.Related_User));
                    //eArg.EventVariables.Add("%username%", eArg.Related_User.Username);
                    //eArg.EventVariables.Add("%email%", eArg.Related_User.Email);
                    //eArg.EventVariables.Add("%usermobile%", string.IsNullOrEmpty(eArg.Related_User.Mobile) ? eArg.Related_User.Tel : eArg.Related_User.Mobile);

                    ////Details Of Order
                    //eArg.EventVariables.Add("%phone%", online.Order.Tel);
                    //eArg.EventVariables.Add("%address%", string.IsNullOrEmpty(online.Order.Address) ? eArg.Related_User.Address : online.Order.Address);
                    //eArg.EventVariables.Add("%description%", online.Order.Comment);
                    //eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    //eArg.EventVariables.Add("%refnum%", online.RefNum);
                    //eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    //eArg.EventVariables.Add("%branch%", online.Order.Branch_ID.HasValue ? online.Order.Branch.Title : "ندارد");
                    //eArg.EventVariables.Add("%orderdate%", DateUtil.GetPersianDateShortString(online.Order.OrderDate));
                    //eArg.EventVariables.Add("%ordertime%", online.Order.OrderDate.TimeOfDay.ToString().Substring(0, 8));
                    //string productlist = "";

                    //foreach (var item in online.Order.OrderDetails)
                    //{
                    //    string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                    //    productlist += temp;
                    //}
                    //if (productlist.Length > 1)
                    //    productlist = productlist.Substring(1);

                    //eArg.EventVariables.Add("%productlist%", productlist);


                    //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(online.Date));
                    //eArg.EventVariables.Add("%time%", online.Date.TimeOfDay.ToString().Substring(0, 8));


                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_New, eArg);

                    //#endregion
                }
            }
            catch (Exception)
            {
                op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                op.Result = ActionResult.Error;
            }
            return op;
        }


        //Step 3 Final Function For Paying
        public OperationResult BranchVerifyTransaction(string RefNum)
        {
            OperationResult op = new OperationResult();
            op.Result = ActionResult.Done;
            try
            {
                ///For Ignore SSL Error
                ServicePointManager.ServerCertificateValidationCallback =
                            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                PaymentIFBinding Saman = new PaymentIFBinding();

                pgcEntities context = new pgcEntities();
                BranchPayment online = context.BranchPayments.SingleOrDefault(f => f.OnlineRefNum == RefNum);


                long result = 0;

                for (int i = 0; i < OptionBusiness.GetInt(OptionKey.TryNumberForVerify); i++)
                {
                    try
                    {
                        result = (long)Saman.verifyTransaction(RefNum, MerchantID);
                        //TEMPORARY
                        //result = online.Amount;
                        i = OptionBusiness.GetInt(OptionKey.TryNumberForVerify);
                    }
                    catch (Exception)
                    { }
                }

                if (result == 0)
                {
                    online.OnlineResultTransaction = result;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                    op.Result = ActionResult.Failed;
                }
                else if (result < 0)
                {
                    online.OnlineResultTransaction = result;                    
                    context.SaveChanges();
                    UserMessageKey key = (UserMessageKey)Enum.Parse(typeof(UserMessageKey), string.Format("Err_{0}", Math.Abs(result)));
                    op.AddMessage(key);
                    op.Result = ActionResult.Error;


                    ////OnlinePayment_Verify
                    #region OnlinePayment_Verify

                    //SystemEventArgs eArg = new SystemEventArgs();
                    //User doer = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //if (UserSession.IsUserLogined)
                    //{
                    //    doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
                    //    if (UserSession.UserID != online.Order.User_ID)
                    //        eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);
                    //}

                    //eArg.Related_Doer = doer;

                    //if (online.Order.Branch_ID.HasValue)
                    //    eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    //eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(doer));
                    //eArg.EventVariables.Add("%username%", doer.Username);
                    //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    //eArg.EventVariables.Add("%mobile%", doer.Mobile);
                    //eArg.EventVariables.Add("%email%", doer.Email);

                    //eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    //eArg.EventVariables.Add("%refnum%", online.RefNum);
                    //eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    //eArg.EventVariables.Add("%state%", UserMessageKeyBusiness.GetUserMessageKey(key));


                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Verify, eArg);

                    #endregion




                    return op;
                }
                else if (result != online.Amount)
                {
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.NeedToReverse);



                    ////OnlinePayment_Verify
                    #region OnlinePayment_Verify

                    //SystemEventArgs eArg = new SystemEventArgs();
                    //User doer = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);

                    //if (UserSession.IsUserLogined)
                    //{
                    //    doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
                    //    if (UserSession.UserID != online.Order.User_ID)
                    //        eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(online.Order.User_ID);
                    //}

                    //eArg.Related_Doer = doer;

                    //if (online.Order.Branch_ID.HasValue)
                    //    eArg.Related_Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);

                    //eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(doer));
                    //eArg.EventVariables.Add("%username%", doer.Username);
                    //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    //eArg.EventVariables.Add("%mobile%", doer.Mobile);
                    //eArg.EventVariables.Add("%email%", doer.Email);

                    //eArg.EventVariables.Add("%orderid%", online.Order.ID.ToString());
                    //eArg.EventVariables.Add("%refnum%", online.RefNum);
                    //eArg.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    //eArg.EventVariables.Add("%state%", UserMessageKeyBusiness.GetUserMessageKey(UserMessageKey.NeedToReverse));


                    //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.OnlinePayment_Verify, eArg);

                    #endregion

                    //OperationResult reverseResult = ReversTransaction(result, RefNum);
                    //if (reverseResult.Result == ActionResult.Done)
                    //    op.Messages.AddRange(reverseResult.Messages);


                    op.Result = ActionResult.Error;
                    return op;
                }
                else if (result == online.Amount)
                {
                    online.OnlineResultTransaction = result;                    
                    online.Type = (int)BranchPaymentType.Online;
                    context.SaveChanges();
                    op.AddMessage(UserMessageKey.OnlineVerificationCompleted);

                    //Finance Cycle Region
                    if (new pgc.Business.BranchTransactionBusiness().InsertBranchOnlineTransaction(online.ID).Result != ActionResult.Done)
                        op.AddMessage(UserMessageKey.PleaseNotifyCenterForInsertOnlineTransaction);



                    //Event Rasing
                    #region BranchPayment_Online_Verify

                    SystemEventArgs eArg = new SystemEventArgs();

                    eArg.Related_Branch = new BranchBusiness().Retrieve(online.Branch_ID);                    

                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%refnum%", online.OnlineRefNum);
                    eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Online_Verify, eArg);

                    #endregion



                }

                return op;
            }
            catch (Exception)
            {

                op.AddMessage(UserMessageKey.FailedOnlineTransaction);
                op.Result = ActionResult.Failed;
                return op;
            }
        }

        #endregion
    }
}