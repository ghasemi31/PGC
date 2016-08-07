using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using pgc.Business.Core;
using kFrameWork.Util;
using kFrameWork.UI;

namespace pgc.Business
{
    public class BranchTransactionBusiness : BaseEntityManagementBusiness<BranchTransaction, pgcEntities>
    {
        public BranchTransactionBusiness()
        {
            Context = new pgcEntities();
        }


        #region Transaction List

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchTransactionPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchTransactions, Pattern)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    Title = f.Branch.Title,
                    f.ID,
                    f.RegDate,
                    f.RegPersianDate,
                    f.TransactionType,
                    f.TransactionType_ID,
                    f.BranchCredit,
                    f.BranchDebt,
                    f.Description                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public long GetTotalBranchCredit(BranchTransactionPattern Pattern)
        {
            var list = Search_Where(Context.BranchTransactions, Pattern);
            if (list.Count() > 0)
                return list.Sum(f => f.BranchCredit - f.BranchDebt);
            else
                return 0;
        }

        public int Search_Count(BranchTransactionPattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).Count();
        }

        public IQueryable<BranchTransaction> Search_Where(IQueryable<BranchTransaction> list, BranchTransactionPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;
            
            //Pattern.Status
            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (BasePattern.IsEnumAssigned(Pattern.Type))
            {
                switch (Pattern.Type)
                {
                    case BranchTransactionTypeForSearch.CenterInDebt:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment ||
                                               f.TransactionType == (int)BranchTransactionType.BranchReturnOrder ||
                                               f.TransactionType == (int)BranchTransactionType.BranchLackOrder ||
                                               f.TransactionType == (int)BranchTransactionType.CustomerOnline);
                        break;
                    
                    
                    case BranchTransactionTypeForSearch.CenterInCredit:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder);
                        break;
                    
                    
                    case BranchTransactionTypeForSearch.BranchOrder:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchOrder);
                        break;


                    case BranchTransactionTypeForSearch.BranchLackOrder:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchLackOrder);
                        break;


                    case BranchTransactionTypeForSearch.BranchReturnOrder:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchReturnOrder);
                        break;


                    case BranchTransactionTypeForSearch.BranchPayment:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment);
                        break;


                    case BranchTransactionTypeForSearch.BranchPaymentOnline:
                        List<long> OnlineTempIDs = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment).Select(f => f.TransactionType_ID).ToList();
                        List<long> OnlineResultIDs = Context.BranchPayments.Where(f => OnlineTempIDs.Any(g => g == f.ID) && f.Type == (int)BranchPaymentType.Online).Select(f => f.ID).ToList();
                        
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment && OnlineResultIDs.Any(g => g == f.TransactionType_ID));
                        break;


                    case BranchTransactionTypeForSearch.BranchPaymentOffline:
                        List<long> OfflineTempIDs = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment).Select(f => f.TransactionType_ID).ToList();
                        List<long> OfflineResultIDs = Context.BranchPayments.Where(f => OfflineTempIDs.Any(g => g == f.ID) && f.Type == (int)BranchPaymentType.Offline).Select(f => f.ID).ToList();
                        
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.BranchPayment && OfflineResultIDs.Any(g => g == f.TransactionType_ID));
                        break;


                    case BranchTransactionTypeForSearch.CustomerPaymentOnline:
                        list = list.Where(f => f.TransactionType == (int)BranchTransactionType.CustomerOnline);
                        break;

                    case BranchTransactionTypeForSearch.ManualCharge:
                        list = list.Where(f=>f.TransactionType==(int)BranchTransactionType.ManualCharge);
                        break;

                    
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f =>  f.Description.Contains(Pattern.Title) ||
                                        f.Branch.Title.Contains(Pattern.Title));
            
            switch (Pattern.BranchCreditPrice.Type)
            {
                case RangeType.Between:
                    if (Pattern.BranchCreditPrice.HasFirstNumber && Pattern.BranchCreditPrice.HasSecondNumber)
                        list = list.Where(f => f.BranchCredit >= Pattern.BranchCreditPrice.FirstNumber
                            && f.BranchCredit <= Pattern.BranchCreditPrice.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.BranchCreditPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit >= Pattern.BranchCreditPrice.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.BranchCreditPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit <= Pattern.BranchCreditPrice.FirstNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.BranchCreditPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchCredit == Pattern.BranchCreditPrice.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }


            switch (Pattern.BranchDebtPrice.Type)
            {
                case RangeType.Between:
                    if (Pattern.BranchDebtPrice.HasFirstNumber && Pattern.BranchDebtPrice.HasSecondNumber)
                        list = list.Where(f => f.BranchDebt >= Pattern.BranchDebtPrice.FirstNumber
                            && f.BranchDebt <= Pattern.BranchDebtPrice.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.BranchDebtPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchDebt >= Pattern.BranchDebtPrice.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.BranchDebtPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchDebt <= Pattern.BranchDebtPrice.FirstNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.BranchDebtPrice.HasFirstNumber)
                        list = list.Where(f => f.BranchDebt == Pattern.BranchDebtPrice.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }


            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.RegPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                    break;
            }

            return list;
        }

        public IQueryable<BranchTransaction> Search_SelectPrint(BranchTransactionPattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).OrderByDescending(f => f.RegDate);
        }



        #endregion




        #region Transactions Functions For Other Business Classes

        public OperationResult InsertCustomerOnlineTransaction(long Online_ID)
        {
            //BranchCurrentCredit_Change

            OperationResult op = new OperationResult();
            try
            {
                if (Online_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                OnlinePayment online = Context.OnlinePayments.SingleOrDefault(f => f.ID == Online_ID);

                if (online == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }


                long oldCredit = 0;
                Branch Branch = new Branch();

                if (online.Order.Branch_ID.HasValue)
                {
                    transaction.Branch_ID = online.Order.Branch_ID.Value;
                    Branch = new BranchBusiness().Retrieve(online.Order.Branch_ID.Value);
                    oldCredit = BranchCreditBusiness.GetBranchCredit(Branch.ID);
                }

                transaction.BranchCredit = online.Amount;
                transaction.BranchDebt = 0;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionCustomerOnlineDesc).Replace("%name%", online.Order.Branch.Title);
                transaction.TransactionType = (int)BranchTransactionType.CustomerOnline;
                transaction.TransactionType_ID = online.ID;
                transaction.RegDate = DateTime.Now;
                transaction.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);


                op= Insert(transaction);



                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID>0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();

                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit + online.Amount) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.CustomerOnline));


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion


                return op;

            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For Online_Customer_Online ");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertBranchOnlineTransaction(long BranchOnline_ID)
        {
            OperationResult op = new OperationResult();
            try
            {
                if (BranchOnline_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                BranchPayment online = Context.BranchPayments.SingleOrDefault(f => f.ID == BranchOnline_ID);

                if (online == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }

                transaction.Branch_ID = online.Branch_ID;
                transaction.BranchCredit = online.Amount;
                transaction.BranchDebt = 0;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionBranchOnlineDesc).Replace("%name%", online.Branch.Title);
                transaction.TransactionType = (int)BranchTransactionType.BranchPayment;
                transaction.TransactionType_ID = online.ID;
                transaction.RegDate = DateTime.Now;
                transaction.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);


                long oldCredit = BranchCreditBusiness.GetBranchCredit(online.Branch_ID);
                
                op= Insert(transaction);



                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID > 0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();
                    Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit + online.Amount) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(online.Amount) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.BranchPayment) +" (آنلاین)");


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion



                return op;
            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For Online_Branch_Online ");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertBranchOfflineTransaction(long BranchOffline_ID)
        {
            OperationResult op = new OperationResult();
            try
            {
                if (BranchOffline_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                BranchPayment offline = Context.BranchPayments.SingleOrDefault(f => f.ID == BranchOffline_ID);

                if (offline == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidOnlineID);
                    return op;
                }

                long oldCredit=BranchCreditBusiness.GetBranchCredit(offline.Branch_ID);


                transaction.Branch_ID = offline.Branch_ID;
                transaction.BranchCredit = offline.Amount;
                transaction.BranchDebt = 0;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionBranchOfflineDesc).Replace("%name%", offline.Branch.Title);
                transaction.TransactionType = (int)BranchTransactionType.BranchPayment;
                transaction.TransactionType_ID = offline.ID;
                transaction.RegDate = DateUtil.GetEnglishDateTime(offline.OfflineReceiptPersianDate);
                transaction.RegPersianDate = offline.OfflineReceiptPersianDate;

                
                op= Insert(transaction);



                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID > 0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();
                    Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit + offline.Amount) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(offline.Amount) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.BranchPayment) + " (آفلاین)");


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion



                return op;

            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For Offline_Branch_Online ");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertFacturOfBranchOrder(long BranchOrder_ID)
        {
            OperationResult op = new OperationResult();
            try
            {
                if (BranchOrder_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                BranchOrder order = Context.BranchOrders.SingleOrDefault(f => f.ID == BranchOrder_ID);

                if (order == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                long oldCredit = BranchCreditBusiness.GetBranchCredit(order.Branch_ID);

                transaction.Branch_ID = order.Branch_ID;
                transaction.BranchCredit = 0;
                transaction.BranchDebt = order.TotalPrice;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionFactureOfBranchOrderDesc).Replace("%name%", order.Branch.Title).Replace("%id%", order.ID.ToString());
                transaction.TransactionType = (int)BranchTransactionType.BranchOrder;
                transaction.TransactionType_ID = order.ID;
                transaction.RegDate = order.RegDate;
                transaction.RegPersianDate = order.RegPersianDate;

                
                op= Insert(transaction);



                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID > 0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();
                    Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit - order.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(order.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.BranchOrder) );


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion





                return op;

            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For BranchOrder_Facture ");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertFacturOfBranchReturnOrder(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();
            try
            {
                if (ReturnOrder_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                BranchReturnOrder returnOrder = Context.BranchReturnOrders.SingleOrDefault(f => f.ID == ReturnOrder_ID);

                if (returnOrder == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                long oldCredit = BranchCreditBusiness.GetBranchCredit(returnOrder.Branch_ID);//returnOrder.BranchOrder.Branch_ID);

                transaction.Branch_ID = returnOrder.Branch_ID;//returnOrder.BranchOrder.Branch_ID;
                transaction.BranchCredit = returnOrder.TotalPrice;
                transaction.BranchDebt = 0;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionFactureOfBranchReturnOrderDesc).Replace("%name%", returnOrder.Branch.Title).Replace("%id%", ReturnOrder_ID.ToString());
                transaction.TransactionType = (int)BranchTransactionType.BranchReturnOrder;
                transaction.TransactionType_ID = returnOrder.ID;
                transaction.RegDate = returnOrder.RegDate;
                transaction.RegPersianDate = returnOrder.RegPersianDate;

                
                op= Insert(transaction);


                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID > 0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();
                    Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit + returnOrder.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.BranchOrder));


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion


                return op;
            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For ReturnBranchOrder_Facture");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertFacturOfBranchLackOrder(long LackOrder_ID)
        {
            OperationResult op = new OperationResult();
            try
            {
                if (LackOrder_ID <= 0)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                BranchTransaction transaction = new BranchTransaction();

                BranchLackOrder lackOrder = Context.BranchLackOrders.SingleOrDefault(f => f.ID == LackOrder_ID);

                if (lackOrder == null)
                {
                    op.Result = ActionResult.Error;
                    op.Messages.Add(Model.Enums.UserMessageKey.InvalidBranchOrderID);
                    return op;
                }

                long oldCredit = BranchCreditBusiness.GetBranchCredit(lackOrder.BranchOrder.Branch_ID);//returnOrder.BranchOrder.Branch_ID);

                transaction.Branch_ID = lackOrder.BranchOrder.Branch_ID;//returnOrder.BranchOrder.Branch_ID;
                transaction.BranchCredit = lackOrder.TotalPrice;
                transaction.BranchDebt = 0;
                transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionFactureOfBranchLackOrderDesc).Replace("%name%", lackOrder.BranchOrder.Branch.Title).Replace("%id%",LackOrder_ID.ToString()).Replace("%oid%",lackOrder.BranchOrder_ID.ToString());
                transaction.TransactionType = (int)BranchTransactionType.BranchLackOrder;
                transaction.TransactionType_ID = lackOrder.ID;
                transaction.RegDate = lackOrder.RegDate;
                transaction.RegPersianDate = lackOrder.RegPersianDate;


                op = Insert(transaction);


                //Event Raising
                #region BranchCurrentCredit_Change
                if (op.Result == ActionResult.Done && transaction.Branch_ID > 0)
                {
                    SystemEventArgs eArg = new SystemEventArgs();
                    Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit + lackOrder.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(lackOrder.TotalPrice) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                    eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                    eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(BranchTransactionType.BranchOrder));


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
                }
                #endregion


                return op;
            }
            catch (Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For LackBranchOrder_Facture");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult InsertBranchManualCharge(BranchTransaction tr)
        {
            OperationResult op = new OperationResult();

            try
            {
                BranchTransaction transaction = new BranchTransaction();
                transaction.Branch_ID = tr.Branch_ID;
                transaction.BranchCredit = tr.BranchCredit;
                transaction.BranchDebt = tr.BranchDebt;
                transaction.Description = tr.Description;
                transaction.RegDate = DateTime.Now;
                transaction.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                transaction.TransactionType = (int)BranchTransactionType.ManualCharge;
                transaction.TransactionType_ID = 0;

                op = Insert(transaction);

                return op;
            }
            catch(Exception ex)
            {
                pgc.Business.Core.ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchTransactionBusiness, Insert Transactio For Manual Charge ");
                op.Result = ActionResult.Failed;
                op.Messages.Add(Model.Enums.UserMessageKey.Failed);
                return op;
            }
        }

        public OperationResult DeleteTransaction(BranchTransactionType branchTransactionType, long TransactionType_ID)
        {
            OperationResult op = new OperationResult();

            BranchTransaction transaction = Context.BranchTransactions.FirstOrDefault(f => f.TransactionType == (int)branchTransactionType && f.TransactionType_ID == TransactionType_ID);


            long oldCredit = 0;

            if (transaction != null)
                oldCredit = BranchCreditBusiness.GetBranchCredit(transaction.Branch_ID);
            else
            {
                op.Result = ActionResult.Done;
                return op;
            }


            switch (branchTransactionType)
            {

                //CustomerOnline
                case BranchTransactionType.CustomerOnline:
                    //OnlinePayment Of Customer Cant Delete So Dont Remind Here
                    break;


                //BranchOrder
                case BranchTransactionType.BranchOrder:

                    BranchOrder BranchOrder = Context.BranchOrders.SingleOrDefault(f => f.ID == TransactionType_ID);

                    op = Delete(transaction.ID);

                    break;



                //BranchPayment
                case BranchTransactionType.BranchPayment:

                    BranchPayment OfflinePayment = Context.BranchPayments.SingleOrDefault(f => f.ID == TransactionType_ID);

                    if (OfflinePayment.Type == (int)BranchPaymentType.Offline && OfflinePayment.OfflinePaymentStatus == (int)BranchOfflinePaymentStatus.Paid)
                        op = Delete(transaction.ID);

                    break;


                //BranchReturnOrder
                case BranchTransactionType.BranchReturnOrder:

                    BranchReturnOrder order = Context.BranchReturnOrders.SingleOrDefault(f => f.ID == TransactionType_ID);

                    op = Delete(transaction.ID);

                    break;


                //BranchLackOrder
                case BranchTransactionType.BranchLackOrder:

                    BranchLackOrder lackOrder = Context.BranchLackOrders.SingleOrDefault(f => f.ID == TransactionType_ID);

                    op = Delete(transaction.ID);

                    break;
                default:
                    break;
            }






            //Event Raising
            #region BranchCurrentCredit_Change
            if (op.Result == ActionResult.Done && transaction != null && transaction.Branch_ID > 0)
            {
                SystemEventArgs eArg = new SystemEventArgs();
                Branch Branch = new BranchBusiness().Retrieve(transaction.Branch_ID);


                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%branchtitle%", Branch.Title);
                eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(oldCredit - transaction.BranchCredit + transaction.BranchDebt) + " ریال");
                eArg.EventVariables.Add("%transactionamount%", UIUtil.GetCommaSeparatedOf(transaction.BranchCredit + transaction.BranchDebt) + " ریال");
                eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldCredit) + " ریال");
                eArg.EventVariables.Add("%minimumcredit%", UIUtil.GetCommaSeparatedOf(Branch.MinimumCredit) + " ریال");

                eArg.EventVariables.Add("%action%", EnumUtil.GetEnumElementPersianTitle(branchTransactionType));


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchCurrentCredit_Change, eArg);
            }
            #endregion



            return op;

        }



        public override OperationResult Insert(BranchTransaction Data)
        {
            if (Context.BranchTransactions.Any(f =>
                                                    f.Branch_ID == Data.Branch_ID &&
                                                    f.TransactionType == Data.TransactionType &&
                                                    f.TransactionType_ID == Data.TransactionType_ID &&
                                                    f.TransactionType != (int)BranchTransactionType.ManualCharge))
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.Messages.Add(UserMessageKey.Failed);

                ExceptionHandler.HandleManualException(new Exception("Duplicate Transaction, Data.TransactionType_ID=" + Data.TransactionType_ID.ToString() + ", Data.TransactionType" + ((BranchTransactionType)Data.TransactionType) + ", Branch_ID=" + Data.Branch_ID.ToString()), "pgc.Business.BranchTransactionBusiness, Insert Transactio For LackBranchOrder_Facture");

                return op;
            }

            return base.Insert(Data);
        }


        #endregion

    }
}