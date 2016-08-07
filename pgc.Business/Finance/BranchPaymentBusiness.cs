using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;
using System.Collections.Generic;

namespace pgc.Business
{
    public class BranchPaymentBusiness : BaseEntityManagementBusiness<BranchPayment, pgcEntities>
    {
        public BranchPaymentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchPaymentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchPayments, Pattern)
                .OrderByDescending(f => f.Date);
            
            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public long TotalAmount(BranchPaymentPattern Pattern)
        {
            var Result=Search_Where(Context.BranchPayments, Pattern);
            if (Result.Count() > 0)
                return Result.Sum(f => f.Amount);
            else
                return 0;
        }

        public int Search_Count(BranchPaymentPattern Pattern)
        {
            return Search_Where(Context.BranchPayments, Pattern).Count();
        }

        public IQueryable<BranchPayment> Search_Where(IQueryable<BranchPayment> list, BranchPaymentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);

            if (Pattern.DefaultSearch)
            {
                list = list.Where(f => (f.Type == (int)BranchPaymentType.Offline && f.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.NotPaid) ||
                                        (f.Type == (int)BranchPaymentType.Online && f.OnlineTransactionState == "OK" && f.OnlineResultTransaction == f.Amount && f.OnlineRefNum != ""));
            }

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.OfflineBankAccountDescription.Contains(Pattern.Title)||
                                        f.OfflineBankAccountTitle.Contains(Pattern.Title)||
                                        f.OfflineDescription.Contains(Pattern.Title)||
                                        f.OfflineReceiptLiquidator.Contains(Pattern.Title) ||
                                        f.OnlineRefNum.Contains(Pattern.Title) ||
                                        f.OnlineResNum.Contains(Pattern.Title) ||
                                        f.OfflineReceiptNumber.Contains(Pattern.Title));

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (BasePattern.IsEnumAssigned(Pattern.Type))
            {
                switch (Pattern.Type)
                {
                    case BranchPaymentTypeForSearch.Online:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Online);
                        break;
                    case BranchPaymentTypeForSearch.OnlineSucceed:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Online && f.OnlineTransactionState == "OK" && f.OnlineResultTransaction == f.Amount && f.OnlineRefNum != "");
                        break;
                    case BranchPaymentTypeForSearch.OnlineNotSucceed:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Online && (f.OnlineTransactionState != "OK" || f.OnlineResultTransaction != f.Amount || f.OnlineRefNum == ""));
                        break;
                    case BranchPaymentTypeForSearch.Offline:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Offline);
                        break;
                    case BranchPaymentTypeForSearch.OfflineSucceed:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Offline && f.OfflinePaymentStatus==(int)BranchOfflinePaymentStatus.Paid);
                        break;
                    case BranchPaymentTypeForSearch.OfflineNotSuccedd:
                        list = list.Where(f => f.Type == (int)BranchPaymentType.Offline && f.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.Paid);
                        break;
                    default:
                        break;
                }                
            }

            switch (Pattern.Price.Type)
            {
                case RangeType.Between:
                    if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
                        list = list.Where(f => f.Amount >= Pattern.Price.FirstNumber
                            && f.Amount <= Pattern.Price.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Amount >= Pattern.Price.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Amount <= Pattern.Price.FirstNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Amount == Pattern.Price.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }

            switch (Pattern.PayDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PayDate.HasFromDate && Pattern.PayDate.HasToDate)
                        list = list.Where(f => (f.Type==(int)BranchPaymentType.Online && f.PersianDate.CompareTo(Pattern.PayDate.FromDate) >= 0 && f.PersianDate.CompareTo(Pattern.PayDate.ToDate) <= 0) ||
                                                (f.Type == (int)BranchPaymentType.Offline && f.OfflineReceiptPersianDate.CompareTo(Pattern.PayDate.FromDate) >= 0 && f.OfflineReceiptPersianDate.CompareTo(Pattern.PayDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PayDate.HasDate)
                        list = list.Where(f => (f.Type==(int)BranchPaymentType.Online && f.PersianDate.CompareTo(Pattern.PayDate.Date) >= 0) ||
                                                (f.Type == (int)BranchPaymentType.Offline && f.OfflineReceiptPersianDate.CompareTo(Pattern.PayDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PayDate.HasDate)
                        list = list.Where(f => (f.Type==(int)BranchPaymentType.Online &&  f.PersianDate.CompareTo(Pattern.PayDate.Date) <= 0)||
                                                (f.Type == (int)BranchPaymentType.Offline && f.OfflineReceiptPersianDate.CompareTo(Pattern.PayDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PayDate.HasDate)
                        list = list.Where(f =>  (f.Type==(int)BranchPaymentType.Online &&f.PersianDate.CompareTo(Pattern.PayDate.Date) == 0)||
                                            (f.Type == (int)BranchPaymentType.Offline && f.OfflineReceiptPersianDate.CompareTo(Pattern.PayDate.Date) == 0));
                    break;
            }


            return list;
        }

        public IQueryable<BranchPayment> Search_SelectPrint(BranchPaymentPattern Pattern)
        {
            return Search_Where(Context.BranchPayments, Pattern).OrderByDescending(f => f.Date);
        }



        public BranchPayment RetrieveByRefNum(string refrenceNumber)
        {
            return Context.BranchPayments.SingleOrDefault(f => f.OnlineRefNum == refrenceNumber);
        }

        public override OperationResult Delete(long ID)
        {
            BranchPayment offline = Retrieve(ID);

            OperationResult op = new OperationResult();

            if (Context.BranchTransactions.Where(f => f.TransactionType_ID == ID && f.TransactionType == (int)BranchTransactionType.BranchPayment).Count() > 0)
            {
                op.AddMessage(UserMessageKey.PreventDeletBranchPaymentCauseHasTransaction);
                op.Result = ActionResult.Failed;
                return op;
            }

            op = base.Delete(ID);


            

            //Evet Raising
            if (op.Result == ActionResult.Done && offline.Type == (int)BranchPaymentType.Offline)
            {
                #region BranchPayment_Offline_Action

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(offline.Branch_ID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));


                eArg.EventVariables.Add("%action%", "حذف");
                eArg.EventVariables.Add("%reciept%", offline.OfflineReceiptNumber);
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%bankaccount%", offline.OfflineBankAccountTitle);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(offline.Amount) + " ریال");
                eArg.EventVariables.Add("%state%", EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)offline.OfflinePaymentStatus));


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Offline_Action, eArg);

                #endregion
            }

            return op;
        }

        public override OperationResult Update(BranchPayment Data)
        {
            OperationResult op= base.Update(Data);



            //Evet Raising
            if (op.Result == ActionResult.Done && Data.Type==(int)BranchPaymentType.Offline)
            {
                #region BranchPayment_Offline_Action

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));


                eArg.EventVariables.Add("%action%", "ویرایش");
                eArg.EventVariables.Add("%reciept%", Data.OfflineReceiptNumber);
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%bankaccount%", Data.OfflineBankAccountTitle);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Amount) + " ریال");
                eArg.EventVariables.Add("%state%", EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)Data.OfflinePaymentStatus));


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Offline_Action, eArg);

                #endregion
            }


            return op;
            
        }

        public override OperationResult Insert(BranchPayment Data)
        {
            OperationResult op= base.Insert(Data);


            //Evet Raising
            if (op.Result == ActionResult.Done && Data.Type == (int)BranchPaymentType.Offline)
            {
                #region BranchPayment_Offline_Action

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));


                eArg.EventVariables.Add("%action%", "ایجاد");
                eArg.EventVariables.Add("%reciept%", Data.OfflineReceiptNumber);
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%bankaccount%", Data.OfflineBankAccountTitle);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Amount) + " ریال");
                eArg.EventVariables.Add("%state%", EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)Data.OfflinePaymentStatus));


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Offline_Action, eArg);

                #endregion
            }



            return op;
        }

        public override OperationResult BulkDelete(List<long> IDs)
        {
            OperationResult Res = new OperationResult();
            int SucceedCount = 0;
            List<UserMessageKey> msg = new List<UserMessageKey>();

            foreach (long ID in IDs)
            {
                OperationResult SingleDeleteResult = Delete(ID);
                if (SingleDeleteResult.Result == ActionResult.Done)
                    SucceedCount++;
                msg.AddRange(SingleDeleteResult.Messages);
            }

            foreach (var item in msg.Distinct())
            {
                Res.AddMessage(item);
            }

            if (SucceedCount == 0)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.Failed);
            }
            else if (SucceedCount < IDs.Count)
            {
                Res.Result = ActionResult.DonWithFailure;
                Res.AddMessage(UserMessageKey.Failed);
            }
            else
            {
                Res.Result = ActionResult.Done;
                Res.AddMessage(UserMessageKey.Succeed);
            }
            Res.Data.Add("RowsAffected", SucceedCount);
            return Res;
        }

        public OperationResult ConfirmPayment(long ID)
        {
            OperationResult op = new OperationResult();

            BranchPayment Data = base.Retrieve(ID);

            if (Data.Type == (int)BranchPaymentType.Offline)
            {
                if (Data.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.Paid)
                {
                    Data.OfflinePaymentStatus = (int)BranchOfflinePaymentStatus.Paid;
                    op = Update(Data);
                    if (op.Result == ActionResult.Done)
                    {



                        //Evet Raising
                        if (op.Result == ActionResult.Done && Data.Type == (int)BranchPaymentType.Offline)
                        {
                            #region BranchPayment_Offline_Confirm

                            SystemEventArgs eArg = new SystemEventArgs();

                            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);

                            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));


                            eArg.EventVariables.Add("%reciept%", Data.OfflineReceiptNumber);
                            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                            eArg.EventVariables.Add("%bankaccount%", Data.OfflineBankAccountTitle);
                            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Amount) + " ریال");                            


                            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Offline_Confirm, eArg);

                            #endregion
                        }



                        op = new BranchTransactionBusiness().InsertBranchOfflineTransaction(Data.ID);

                        if (op.Result == ActionResult.Done)
                        {
                            op = new OperationResult();
                            op.AddMessage(UserMessageKey.SucceedAndInsertTransaction);
                            op.Result = ActionResult.Done;
                            return op;
                        }
                        else
                            return op;
                    }
                    else
                        return op;
                }
                op.Result = ActionResult.Done;
                op.AddMessage(UserMessageKey.Succeed);
                return op;
            }
            else
            {
                op.AddMessage(UserMessageKey.Failed);
                op.Result = ActionResult.Failed;
                return op;
            }
        }

        public OperationResult UnConfirmPayment(long ID)
        {
            OperationResult op = new OperationResult();

            BranchPayment Data = base.Retrieve(ID);

            if (Data.Type == (int)BranchPaymentType.Offline)
            {
                if (Data.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.NotPaid)
                {
                    Data.OfflinePaymentStatus = (int)BranchOfflinePaymentStatus.NotPaid;
                    op = Update(Data);
                    if (op.Result == ActionResult.Done)
                    {




                        //Evet Raising
                        if (op.Result == ActionResult.Done && Data.Type == (int)BranchPaymentType.Offline)
                        {
                            #region BranchPayment_Offline_Confirm

                            SystemEventArgs eArg = new SystemEventArgs();

                            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);

                            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));


                            eArg.EventVariables.Add("%reciept%", Data.OfflineReceiptNumber);
                            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                            eArg.EventVariables.Add("%bankaccount%", Data.OfflineBankAccountTitle);
                            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Amount) + " ریال");


                            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPayment_Offline_UnConfirm, eArg);

                            #endregion
                        }




                        op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchPayment, Data.ID);
                        if (op.Result == ActionResult.Done)
                        {
                            op = new OperationResult();
                            op.AddMessage(UserMessageKey.SucceedAndInsertTransaction);
                            op.Result = ActionResult.Done;
                        }
                        return op;
                    }
                    else
                        return op;
                }
                op.Result = ActionResult.Done;
                op.AddMessage(UserMessageKey.Succeed);
                return op;
            }
            else
            {
                op.AddMessage(UserMessageKey.Failed);
                op.Result = ActionResult.Failed;
                return op;
            }
        }
    }
}