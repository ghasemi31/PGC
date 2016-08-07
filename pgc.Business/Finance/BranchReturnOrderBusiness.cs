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
    public class BranchReturnOrderBusiness : BaseEntityManagementBusiness<BranchReturnOrder, pgcEntities>
    {
        public BranchReturnOrderBusiness()
        {
            Context = new pgcEntities();
        }


        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchReturnOrderPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchReturnOrders, Pattern)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {                    
                    BranchTitle=f.Branch.Title,
                    f.ID,
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

        public int Search_Count(BranchReturnOrderPattern Pattern)
        {
            return Search_Where(Context.BranchReturnOrders, Pattern).Count();
        }

        public IQueryable<BranchReturnOrder> Search_Where(IQueryable<BranchReturnOrder> list, BranchReturnOrderPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                list = list.Where(n => n.ID == Pattern.ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (Pattern.OrderTitle_ID > 0)
                list = list.Where(f => f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            if (Pattern.IsApproved)
                list = list.Where(f => f.Status == (int)BranchReturnOrderStatus.Confirmed || f.Status == (int)BranchReturnOrderStatus.Finalized);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.AdminDescription.Contains(Pattern.Title) ||
                                    f.BranchDescription.Contains(Pattern.Title) ||
                                    f.BranchReturnOrderDetails.Any(g => g.BranchOrderTitle_Title.Contains(Pattern.Title)));

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
                        list = list.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.FromDate) >= 0 && f.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.OrderedPersianDate.HasDate)
                        list = list.Where(f => (f.OrderedPersianDate.CompareTo(Pattern.OrderedPersianDate.Date) == 0));
                    break;
            }

            return list;
        }


        public IQueryable<BranchReturnOrder> Search_SelectPrint(BranchReturnOrderPattern Pattern)
        {
            return Search_Where(Context.BranchReturnOrders, Pattern).OrderByDescending(f => f.RegDate);
        }




        public OperationResult AdminFinalize(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();

            BranchReturnOrder returnOrder = Retrieve(ReturnOrder_ID);

            if (returnOrder.Status == (int)BranchReturnOrderStatus.Confirmed)
            {
                returnOrder.Status = (int)BranchReturnOrderStatus.Finalized;
                op = Update(returnOrder);
            }
            else
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchReturnOrder_PreventAdminFinalizedCauseNotConfirmed);
                return op;
            }

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchReturnOrder_AdminFinalized);





                //Event Rasing
                #region BranchReturnOrder_Finalize

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(returnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", returnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder_ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in returnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_Finalize, eArg);

                #endregion





                //INSERTING LOG OF RETURNORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminFinalization, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }
            return op;
        }

        public OperationResult AdminConfirmation(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();

            BranchReturnOrder returnOrder = Retrieve(ReturnOrder_ID);

            

            op = new BranchTransactionBusiness().InsertFacturOfBranchReturnOrder(ReturnOrder_ID);

            if (op.Result == ActionResult.Done)
            {
                returnOrder.Status = (int)BranchReturnOrderStatus.Confirmed;
                op = Update(returnOrder);
            }
            else
                return op;


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchReturnOrderAdminConfirmed);





                //Event Rasing
                #region BranchReturnOrder_Confirm

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(returnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", returnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder_ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in returnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_Confirm, eArg);

                #endregion





                //INSERTING LOG OF RETURNORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminConfirmation, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }

            return op;
        }

        public OperationResult AdminCancelation(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();
    
            BranchReturnOrder returnOrder = base.Retrieve(ReturnOrder_ID);




            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchReturnOrder, ReturnOrder_ID);
            
            
            if (op.Result == ActionResult.Done)
            {
                returnOrder.Status = (int)BranchReturnOrderStatus.Canceled;
                op = Update(returnOrder);
            }
            else
                return op;
            
            
            
            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchReturnOrderAdminRejected);




                //Event Rasing
                #region BranchReturnOrder_Cancelation

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(returnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", returnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder_ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in returnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_Cancelation, eArg);

                #endregion





                //INSERTING LOG OF RETURNORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminCanclation, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);


            }
            return op;
        }

        public OperationResult AdminRollBackCancle(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();
            BranchReturnOrder ReturnOrder = Retrieve(ReturnOrder_ID);


            ReturnOrder.Status = (int)BranchReturnOrderStatus.Pending;
            op = Update(ReturnOrder);

            if (op.Result != ActionResult.Done)
                return op;

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                //op.AddMessage(UserMessageKey.BranchReturnOrderAdminCorrection);



                //Event Rasing
                #region BranchReturnOrder_RollBackCancle

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(ReturnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", ReturnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(ReturnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder_ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in ReturnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_RollBackCancle, eArg);

                #endregion




                //Inserting Log Of Rollback
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminRollingBackCanclation, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);


            }
            return op;
        }

        public OperationResult AdminEdit(BranchReturnOrder Data)
        {
            long oldCost = new BranchReturnOrderBusiness().Retrieve(Data.ID).TotalPrice;

            OperationResult op = new OperationResult();


            if (Data.Status != (int)BranchReturnOrderStatus.Pending && Data.Status != (int)BranchReturnOrderStatus.Canceled)
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
                opSucceed.AddMessage(UserMessageKey.BranchReturnOrder_SucceedEditByAdmin);
            }
            else
                return opSucceed;



            //Event Rasing
            #region BranchOrder_AdminEdit

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
            eArg.EventVariables.Add("%oldcost%", UIUtil.GetCommaSeparatedOf(oldCost) + " ریال");
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
            eArg.EventVariables.Add("%id%", Data.ID.ToString());

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchReturnOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchReturnOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in Data.BranchReturnOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_AdminEdit, eArg);

            #endregion





            ////INSERTING LOG OF ORDER CHANGE
            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminEditing, Data.ID, BranchFinanceLogType.BranchReturnOrder);
            if (opLog.Result != ActionResult.Done)
                op.Messages.AddRange(opLog.Messages);


            return opSucceed;
        }

        public OperationResult AdminUpdateBeforeEdit(BranchReturnOrder returnOrder)
        {
            if (returnOrder.ID > 0)
            {
                foreach (var item in returnOrder.BranchReturnOrderDetails.ToList())
                    Context.BranchReturnOrderDetails.DeleteObject(item);

                return Update(returnOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }

        public OperationResult AdminRollBackFinalized(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();
            BranchReturnOrder ReturnOrder = Retrieve(ReturnOrder_ID);


            ReturnOrder.Status = (int)BranchReturnOrderStatus.Confirmed;
            op = Update(ReturnOrder);

            if (op.Result != ActionResult.Done)
                return op;

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchReturnOrderAdminRollingBackFinalized);



                //Event Rasing
                #region BranchReturnOrder_RollBackFinalized

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(ReturnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", ReturnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(ReturnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in ReturnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_RollBackFinalized, eArg);

                #endregion


                //Inserting Log Of Rollback
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminRollingBackFinalized, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);


            }
            return op;
        }

        public OperationResult AdminDelete(long ReturnOrder_ID)
        {
            OperationResult op = new OperationResult();
            BranchReturnOrder ReturnOrder = Retrieve(ReturnOrder_ID);

            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchReturnOrder, ReturnOrder_ID);

            if (op.Result != ActionResult.Done)
            {
                OperationResult opTransaction = new OperationResult();
                opTransaction.Result = op.Result;
                opTransaction.AddMessage(UserMessageKey.BranchTransactionDeleteFailed);
                return opTransaction;
            }


            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAdminDelete, ReturnOrder_ID, BranchFinanceLogType.BranchReturnOrder);


            op = base.Delete(ReturnOrder.ID);


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchReturnOrderAdminDelete);



                //Event Rasing
                #region BranchReturnOrder_AdminDelete

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(ReturnOrder.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", ReturnOrder.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(ReturnOrder.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
                eArg.EventVariables.Add("%id%", ReturnOrder.ID.ToString());

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(ReturnOrder.BranchReturnOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in ReturnOrder.BranchReturnOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_AdminDelete, eArg);

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




        public bool IsOpenForAgentAction(long ReturnOrder_ID)
        {
            BranchReturnOrder ReturnOrder = Retrieve(ReturnOrder_ID);

            string TimeForAcceptStart = OptionBusiness.GetText(OptionKey.BranchReturnOrderNew_AcceptFrom);
            string TimeForAcceptEnd = OptionBusiness.GetText(OptionKey.BranchReturnOrderNew_AcceptTo);
            string CurrentTime = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);


            return (ReturnOrder.Status == (int)BranchReturnOrderStatus.Pending &&
                    ReturnOrder.OrderedPersianDate == DateUtil.GetPersianDateShortString(DateTime.Now) &&
                    CurrentTime.CompareTo(TimeForAcceptEnd) < 0 &&
                    CurrentTime.CompareTo(TimeForAcceptStart) > 0);
        }
        
        public OperationResult AgentInsert(BranchReturnOrder Data)
        {
            OperationResult op = new OperationResult();

            if (Data.Status != (int)BranchReturnOrderStatus.Pending)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchReturnOrder_PreventAdminEditCauseNotPending);
                return op;
            }


            bool isEditing = Data.ID > 0;

            OperationResult opSucceed = new OperationResult();

            if (isEditing)
                opSucceed = base.Update(Data);
            else
                opSucceed = base.Insert(Data);

            if (opSucceed.Result == ActionResult.Done)
            {
                opSucceed.Messages.Clear();
                opSucceed.AddMessage(UserMessageKey.BranchReturnOrderNew_SucceedByBranch);
            }



            //Event Rasing
            #region BranchReturnOrder_Insert

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
            eArg.EventVariables.Add("%id%", Data.ID.ToString());

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchReturnOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchReturnOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in Data.BranchReturnOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_Insert, eArg);

            #endregion





            ////INSERTING LOG OF ORDER CHANGE

            var opLog = BranchFinanceLogBusiness.InsertLog(
                                                            (isEditing) ? 
                                                                    BranchFinanceLogActionType.BranchReturnOrderAgentEditing 
                                                                    : 
                                                                    BranchFinanceLogActionType.BranchReturnOrderAgentInserting
                                                            , Data.ID
                                                            , BranchFinanceLogType.BranchReturnOrder);
            if (opLog.Result != ActionResult.Done)
                op.Messages.AddRange(opLog.Messages);


            return opSucceed;
        }

        public OperationResult AgentUpdateBeforeInsert(BranchReturnOrder returnOrder)
        {
            if (returnOrder.ID > 0)
            {
                foreach (var item in returnOrder.BranchReturnOrderDetails.ToList())
                    Context.BranchReturnOrderDetails.DeleteObject(item);

                return Update(returnOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }


        public override OperationResult Delete(long ID)
        {
            BranchReturnOrder returnOrder = new BranchReturnOrderBusiness().Retrieve(ID);


            //Insert LOG
            OperationResult logOP = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchReturnOrderAgentDelete, ID, BranchFinanceLogType.BranchReturnOrder);


            //Event Rasing
            #region BranchOrderNew

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(returnOrder.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%deliverdate%", returnOrder.OrderedPersianDate);
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);
            eArg.EventVariables.Add("%id%", returnOrder.ID.ToString());

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(returnOrder.BranchReturnOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in returnOrder.BranchReturnOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);

            #endregion




            if (logOP.Result == ActionResult.Done)
            {
                var deleteOP = base.Delete(ID);

                if (deleteOP.Result != ActionResult.Done)
                {
                    try { new BranchFinanceLogBusiness().Delete((long)logOP.Data["ID"]); }
                    catch (Exception) { }
                    return deleteOP;
                }
                else
                {
                    //FIre Event HERE
                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchReturnOrder_AgentDelete, eArg);
                    
                    return deleteOP;
                }
            }

            return logOP;
        }
        

        public long RetrieveLastProperSinglePrice(long OrderTitle_ID,long Branch_ID)
        {
            var orderList = Context.BranchOrderDetails.Where(f => f.BranchOrder.Branch_ID == Branch_ID && f.BranchOrderTitle_ID == OrderTitle_ID);
            
            if (orderList.Count() > 0)
            {
                return orderList.OrderByDescending(f => f.BranchOrder.OrderedPersianDate).First().SinglePrice;
            }
            else
            {
                var OrderTitle= Context.BranchOrderTitles.SingleOrDefault(f => f.ID == OrderTitle_ID);
                return (OrderTitle == null) ? 0 : OrderTitle.Price;
            }
        }


        public BranchOrderTitle RetrieveOrderTitle(long Title_ID)
        {
            return Context.BranchOrderTitles.FirstOrDefault(f => f.ID == Title_ID);
        }

        public BranchOrder RetrieveOrder(long BranchOrder_ID)
        {
            return Context.BranchOrders.SingleOrDefault(f => f.ID == BranchOrder_ID);
        }

        public long GetNumberOfStatus(BranchReturnOrderStatus branchReturnOrderStatus)
        {
            return Context.BranchReturnOrders.Count(f => f.Status == (int)branchReturnOrderStatus);
        }


        protected string GetProductDetails(IQueryable<BranchReturnOrderDetail> list, bool withMoney)
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
