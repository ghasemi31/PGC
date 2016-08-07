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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using kFrameWork.UI;

namespace pgc.Business
{
    public class BranchLackOrderBusiness : BaseEntityManagementBusiness<BranchLackOrder, pgcEntities>
    {
        public BranchLackOrderBusiness()
        {
            Context = new pgcEntities();
        }


        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchLackOrderPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchLackOrders, Pattern)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    BranchTitle = f.BranchOrder.Branch.Title,
                    f.ID,
                    f.BranchOrder_ID,
                    f.RegDate,
                    f.RegPersianDate,
                    f.OrderedPersianDate,
                    f.Status,
                    f.TotalPrice,
                    f.AdminDescription,
                    f.BranchDescription
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchLackOrderPattern Pattern)
        {
            return Search_Where(Context.BranchLackOrders, Pattern).Count();
        }

        public IQueryable<BranchLackOrder> Search_Where(IQueryable<BranchLackOrder> list, BranchLackOrderPattern Pattern)
        {
            list = list.Where(f => f.BranchOrder.Status == (int)BranchOrderStatus.Confirmed || f.BranchOrder.Status == (int)BranchOrderStatus.Finalized);

            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                list = list.Where(n => n.ID == Pattern.ID);

            if (Pattern.BranchOrder_ID > 0)
                list = list.Where(n => n.BranchOrder_ID == Pattern.BranchOrder_ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.BranchOrder.Branch_ID == Pattern.Branch_ID);

            if (Pattern.OrderTitle_ID > 0)
                list = list.Where(f => f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            if (Pattern.IsApproved)
                //list = list.Where(f => f.Status == (int)BranchLackOrderStatus.Finalized || f.Status == (int)BranchLackOrderStatus.Confirmed);
                list = list.Where(f => f.Status == (int)BranchLackOrderStatus.Confirmed);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.AdminDescription.Contains(Pattern.Title) ||
                                    f.BranchDescription.Contains(Pattern.Title) ||
                                    f.BranchLackOrderDetails.Any(g => g.BranchOrderTitle_Title.Contains(Pattern.Title)));

            switch (Pattern.Price.Type)
            {
                case RangeType.Between:
                    if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
                        list = list.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber
                            && f.TotalPrice <= Pattern.Price.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.TotalPrice >= Pattern.Price.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.TotalPrice <= Pattern.Price.FirstNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.TotalPrice == Pattern.Price.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }

            switch (Pattern.OrderedPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.OrderedPersianDate.HasFromDate && Pattern.OrderedPersianDate.HasToDate)
                        list = list.Where(f => (f.BranchOrder.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.FromDate) >= 0 && f.BranchOrder.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.BranchOrder.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.BranchOrder.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.BranchOrder.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) == 0));
                    break;
            }

            return list;
        }


        public IQueryable<BranchLackOrder> Search_SelectPrint(BranchLackOrderPattern Pattern)
        {
            return Search_Where(Context.BranchLackOrders, Pattern).OrderByDescending(f => f.RegDate);
        }






        public OperationResult AdminConfirmation(long LackOrder_ID)
        {
            OperationResult op = new OperationResult();

            BranchLackOrder LackOrder = Retrieve(LackOrder_ID);

            op = CheckOrderIsConfirmed(LackOrder);

            if (op.Result != ActionResult.Done)
                return op;



            op = new BranchTransactionBusiness().InsertFacturOfBranchLackOrder(LackOrder_ID);

            BranchLackOrder Order = base.Retrieve(LackOrder_ID);

            if (op.Result == ActionResult.Done)
            {
                Order.Status = (int)BranchLackOrderStatus.Confirmed;
                op = base.Update(Order);
            }

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchLackOrder_AdminConfirmed);





                //Event Rasing
                #region BranchLackOrder_Confirm

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.BranchOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", LackOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(LackOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%oid%", LackOrder.BranchOrder_ID.ToString());
                eArg.EventVariables.Add("%id%", LackOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in LackOrder.BranchLackOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_Confirm, eArg);

                #endregion






                //INSERTING LOG OF RETURNORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAdminConfirmation, LackOrder_ID, BranchFinanceLogType.BranchLackOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }
            return op;
        }

        public OperationResult AdminCancelation(long LackOrder_ID)
        {
            OperationResult op = new OperationResult();

            BranchLackOrder LackOrder = base.Retrieve(LackOrder_ID);




            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchLackOrder, LackOrder_ID);

            if (op.Result == ActionResult.Done)
            {
                LackOrder.Status = (int)BranchLackOrderStatus.Canceled;
                op = Update(LackOrder);
            }


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchLackOrder_AdminCanceled);




                //Event Rasing
                #region BranchLackOrder_AdminCancelation

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(LackOrder.BranchOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", LackOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(LackOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%oid%", LackOrder.BranchOrder_ID.ToString());
                eArg.EventVariables.Add("%id%", LackOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in LackOrder.BranchLackOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AdminCancelation, eArg);

                #endregion






                ////INSERTING LOG OF LackOrder CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAdminCanclation, LackOrder_ID, BranchFinanceLogType.BranchLackOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }
            return op;
        }

        public OperationResult AdminRollBackCancle(long LackOrder_ID)
        {
            OperationResult op = new OperationResult();
            BranchLackOrder LackOrder = Retrieve(LackOrder_ID);


            LackOrder.Status = (int)BranchLackOrderStatus.Pending;
            op = Update(LackOrder);


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                //op.AddMessage(UserMessageKey.BranchReturnOrderAdminCorrection);




                //Event Rasing
                #region BranchLackOrder_AdminRollBackCancle

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(LackOrder.BranchOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", LackOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(LackOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%oid%", LackOrder.BranchOrder_ID.ToString());
                eArg.EventVariables.Add("%id%", LackOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in LackOrder.BranchLackOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AdminRollBackCancle, eArg);

                #endregion


                ////INSERTING LOG OF LackOrder CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAdminRollingBackCanclation, LackOrder_ID, BranchFinanceLogType.BranchLackOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }
            return op;
        }

        public OperationResult AdminEdit(BranchLackOrder Data)
        {
            long oldCost = new BranchLackOrderBusiness().Retrieve(Data.ID).TotalPrice;

            OperationResult op = new OperationResult();


            if (Data.BranchOrder.Status != (int)BranchOrderStatus.Confirmed &&
                Data.BranchOrder.Status != (int)BranchOrderStatus.Finalized &&
                Data.Status != (int)BranchReturnOrderStatus.Pending &&
                Data.Status != (int)BranchReturnOrderStatus.Canceled)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchReturnOrder_PreventAdminEditCauseNotPending);
                return op;
            }


            OperationResult opSucceed = new OperationResult();

            if (Data.ID > 0)
                opSucceed = base.Update(Data);
            else
                opSucceed = base.Insert(Data);

            if (opSucceed.Result == ActionResult.Done)
            {
                opSucceed.Messages.Clear();
                opSucceed.AddMessage(UserMessageKey.BranchLackOrder_SucceedEditByAdmin);
            }



            //Event Rasing
            #region BranchLackOrder_AdminEdit

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.BranchOrder.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
            eArg.EventVariables.Add("%oldcost%", UIUtil.GetCommaSeparatedOf(oldCost) + " ریال");
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
            eArg.EventVariables.Add("%oid%", Data.BranchOrder_ID.ToString());
            eArg.EventVariables.Add("%id%", Data.ID.ToString());

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in Data.BranchLackOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AdminEdit, eArg);

            #endregion




            ////INSERTING LOG OF LackOrder CHANGE
            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAdminEditing, Data.ID, BranchFinanceLogType.BranchLackOrder);
            if (opLog.Result != ActionResult.Done)
                op.Messages.AddRange(opLog.Messages);

            return opSucceed;
        }

        public OperationResult AdminUpdateBeforeEdit(BranchLackOrder lackOrder)
        {
            if (lackOrder.ID > 0)
            {
                foreach (var item in lackOrder.BranchLackOrderDetails.ToList())
                    Context.BranchLackOrderDetails.DeleteObject(item);

                return Update(lackOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }

        public OperationResult AdminDelete(long lackOrder_ID)
        {
            OperationResult op = new OperationResult();
            BranchLackOrder LackOrder = new BranchLackOrderBusiness().Retrieve(lackOrder_ID);

            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchLackOrder, lackOrder_ID);

            if (op.Result != ActionResult.Done)
            {
                OperationResult opTransaction = new OperationResult();
                opTransaction.Result = op.Result;
                opTransaction.AddMessage(UserMessageKey.BranchTransactionDeleteFailed);
                return opTransaction;
            }


            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAdminDelete, lackOrder_ID, BranchFinanceLogType.BranchLackOrder);


            op = base.Delete(LackOrder.ID);


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchLackOrderAdminDelete);



                //Event Rasing
                #region BranchLackOrder_AdminDelete

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(LackOrder.BranchOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", LackOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(LackOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%oid%", LackOrder.BranchOrder_ID.ToString());
                eArg.EventVariables.Add("%id%", LackOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(LackOrder.BranchLackOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in LackOrder.BranchLackOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AdminDelete, eArg);

                #endregion


                //Inserting Log Of Rollback
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);


            }
            else
            {
                try { new BranchFinanceLogBusiness().Delete((long)opLog.Data["ID"]); }
                catch (Exception) { }
                return opLog;
            }


            return op;
        }




        public OperationResult AgentInsert(BranchLackOrder Data)
        {
            OperationResult opSucceed = new OperationResult();
            bool isEdit = Data.ID > 0;

            if (Data.ID > 0)
                opSucceed = base.Update(Data);
            else
                opSucceed = base.Insert(Data);


            if (opSucceed.Result == ActionResult.Done)
            {
                opSucceed.Messages.Clear();
                opSucceed.AddMessage(UserMessageKey.BranchLackOrderNew_SucceedByBranch);



                //Event Rasing
                #region BranchLackOrder_AgentInsert

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.BranchOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%oid%", Data.BranchOrder_ID.ToString());
                eArg.EventVariables.Add("%id%", Data.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchLackOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AgentInsert, eArg);

                #endregion




                ////INSERTING LOG OF LackOrder CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(
                                                                (isEdit) ?
                                                                    BranchFinanceLogActionType.BranchLackOrderAgentEditing
                                                                    :
                                                                    BranchFinanceLogActionType.BranchLackOrderAgentInserting,
                                                                Data.ID,
                                                                BranchFinanceLogType.BranchLackOrder);
                if (opLog.Result != ActionResult.Done)
                    opSucceed.Messages.AddRange(opLog.Messages);

            }
            return opSucceed;
        }

        public OperationResult AgentUpdateBeforeInsert(BranchLackOrder lackOrder)
        {
            if (lackOrder.ID > 0)
            {
                foreach (var item in lackOrder.BranchLackOrderDetails.ToList())
                    Context.BranchLackOrderDetails.DeleteObject(item);

                return Update(lackOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }

        public bool IsOpenForAgentAction(long LackOrder_ID)
        {
            BranchLackOrder LackOrder = Retrieve(LackOrder_ID);

            string TimeForAcceptStart = OptionBusiness.GetText(OptionKey.BranchLackOrderNew_AcceptFrom);
            string TimeForAcceptEnd = OptionBusiness.GetText(OptionKey.BranchLackOrderNew_AcceptTo);
            string CurrentTime = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);


            return (LackOrder.Status == (int)BranchLackOrderStatus.Pending &&
                    LackOrder.OrderedPersianDate == DateUtil.GetPersianDateShortString(DateTime.Now) &&
                    CurrentTime.CompareTo(TimeForAcceptEnd) < 0 &&
                    CurrentTime.CompareTo(TimeForAcceptStart) > 0);
        }


        public override OperationResult Delete(long ID)
        {
            OperationResult op = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchLackOrderAgentDelete, ID, BranchFinanceLogType.BranchLackOrder);


            BranchLackOrder Data = new BranchLackOrderBusiness().Retrieve(ID);

            //Event Rasing
            #region BranchLackOrder_AgentDelete

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.BranchOrder.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
            eArg.EventVariables.Add("%oid%", Data.BranchOrder_ID.ToString());
            eArg.EventVariables.Add("%id%", Data.ID.ToString());

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchLackOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in Data.BranchLackOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchLackOrder_AgentDelete, eArg);

            #endregion




            if (op.Result == ActionResult.Done)
            {
                var deleteOP = base.Delete(ID);

                if (deleteOP.Result != ActionResult.Done)
                {
                    try { new BranchFinanceLogBusiness().Delete((long)deleteOP.Data["ID"]); }
                    catch (Exception) { }
                    return deleteOP;
                }
                else
                    return deleteOP;
            }

            return op;
        }


        public BranchOrderTitle RetrieveOrderTitle(long Title_ID)
        {
            return Context.BranchOrderTitles.FirstOrDefault(f => f.ID == Title_ID);
        }

        public BranchLackOrder RetrieveLackByOrder(long BranchOrder_ID)
        {
            return Context.BranchLackOrders.FirstOrDefault(f => f.BranchOrder_ID == BranchOrder_ID);
        }

        public BranchOrder RetrieveOrder(long BranchOrder_ID)
        {
            return Context.BranchOrders.SingleOrDefault(f => f.ID == BranchOrder_ID);
        }

        private OperationResult CheckOrderIsConfirmed(BranchLackOrder LackOrder)
        {
            OperationResult op = new OperationResult();

            if (LackOrder.BranchOrder.Status != (int)BranchOrderStatus.Confirmed)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchLackOrder_ConstraintStatusOfBranchOrder);
                return op;
            }

            op.Result = ActionResult.Done;
            return op;
        }

        public Branch RetrieveBranch(long Branch_ID)
        {
            return Context.Branches.SingleOrDefault(f => f.ID == Branch_ID);
        }

        public long GetNumberOfStatus(BranchLackOrderStatus branchLackOrderStatus)
        {
            return Context.BranchLackOrders.Count(f => f.Status == (int)branchLackOrderStatus);
        }


        protected string GetProductDetails(IQueryable<BranchLackOrderDetail> list, bool withMoney)
        {
            string result = "";

            foreach (var item in list)
            {
                if (withMoney)
                    result += string.Format("{0},{1}({2})",
                                            item.BranchOrderTitle_Title,
                                            UIUtil.GetCommaSeparatedOf(item.Quantity) + " عدد",
                                            UIUtil.GetCommaSeparatedOf(item.TotalPrice) + " ریال");

                else
                    result += string.Format("{0}({1})",
                                            item.BranchOrderTitle_Title,
                                            UIUtil.GetCommaSeparatedOf(item.Quantity) + " عدد");
            }

            return result;
        }
    }
}
