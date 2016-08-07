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
    public class BranchOrderBusiness : BaseEntityManagementBusiness<BranchOrder, pgcEntities>
    {
        public BranchOrderBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchOrders, Pattern)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    Title = f.Branch.Title,
                    f.ID,
                    f.RegDate,
                    f.RegPersianDate,
                    f.OrderedPersianDate,
                    f.Status,
                    f.TotalPrice,
                    f.AdminDescription,
                    f.Branch_ID,
                    HasLack = (f.BranchLackOrders.Count > 0)
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchOrderPattern Pattern)
        {
            return Search_Where(Context.BranchOrders, Pattern).Count();
        }

        public IQueryable<BranchOrder> Search_Where(IQueryable<BranchOrder> list, BranchOrderPattern Pattern)
        {
            //list = list.Where(f => f.Status != (int)BranchOrderStatus.Canceled);

            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                list = list.Where(n => n.ID == Pattern.ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (Pattern.OrderTitle_ID > 0)
                list = list.Where(f => f.BranchOrderDetails.Any(g => g.BranchOrderTitle_ID == Pattern.OrderTitle_ID));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            if (Pattern.IsApproved)
                list = list.Where(f => f.Status == (int)BranchOrderStatus.Confirmed || f.Status == (int)BranchOrderStatus.Finalized);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.AdminDescription.Contains(Pattern.Title) ||
                                    f.BranchDescription.Contains(Pattern.Title) ||
                                    f.BranchOrderDetails.Any(g => g.BranchOrderTitle_Title.Contains(Pattern.Title)));

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

        public IQueryable<BranchOrder> Search_SelectPrint(BranchOrderPattern Pattern)
        {
            return Search_Where(Context.BranchOrders, Pattern).OrderByDescending(f => f.RegDate);
        }

        public OperationResult AdminFinalize(long Order_ID)
        {
            OperationResult op = new OperationResult();

            BranchOrder Order = base.Retrieve(Order_ID);

            if (Order.Status == (int)BranchOrderStatus.Confirmed)
            {
                Order.Status = (int)BranchOrderStatus.Finalized;
                op = Update(Order);
            }
            else
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrder_PreventAdminFinalizedCauseNotConfirmed);
                return op;
            }




            if (op.Result == ActionResult.Done)
            {
                //if (Order.BranchReturnOrders.Count() > 0)
                //    new BranchReturnOrderBusiness().AdminSetPending(Order.BranchReturnOrders.FirstOrDefault().ID);

                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchOrder_AdminFinalized);


                //Event Rasing
                #region BranchOrder_AdminFinalize

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));
                
                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AdminFinalize, eArg);

                #endregion





                ////INSERTING LOG OF ORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminFinalization, Order_ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);
            }
            return op;
        }

        public OperationResult AdminConfirmation(long Order_ID)
        {
            OperationResult op = new OperationResult();

            BranchOrder Order = base.Retrieve(Order_ID);



            op = new BranchTransactionBusiness().InsertFacturOfBranchOrder(Order_ID);


            if (op.Result != ActionResult.Done)
                return op;


            if (op.Result == ActionResult.Done)
            {
                Order.Status = (int)BranchOrderStatus.Confirmed;
                op = Update(Order);
            }

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchOrderAdminConfirmed);




                //Event Rasing
                #region BranchOrder_Confirm

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_Confirm, eArg);

                #endregion





                ////INSERTING LOG OF ORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminConfirmation, Order_ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);
            }
            return op;
        }

        public OperationResult AdminCancelation(long Order_ID)
        {
            OperationResult op = new OperationResult();

            BranchOrder Order = base.Retrieve(Order_ID);




            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchOrder, Order.ID);

            if (op.Result != ActionResult.Done)
                return op;

            if (op.Result == ActionResult.Done && Order.BranchLackOrders.Count() > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchLackOrder, Order.BranchLackOrders.First().ID);
                    i = (op.Result == ActionResult.Done) ? 20 : i;
                }

                new BranchLackOrderBusiness().AdminCancelation(Order.BranchLackOrders.First().ID);
            }

            if (op.Result == ActionResult.Done)
            {
                Order.Status = (int)BranchOrderStatus.Canceled;
                op = Update(Order);

                if (op.Result == ActionResult.Done && Order.BranchLackOrders.Count() > 0)
                    for (int i = 0; i < 20; i++)
                    {
                        op = new BranchLackOrderBusiness().AdminCancelation(Order.BranchLackOrders.First().ID);
                        i = (op.Result == ActionResult.Done) ? 20 : i;
                    }

            }

            if (op.Result == ActionResult.Done)
            {
                //if (Order.BranchReturnOrders.Count() > 0)
                //    new BranchReturnOrderBusiness().AdminReject(Order.BranchReturnOrders.FirstOrDefault().ID);

                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchOrderAdminRejected);




                //Event Rasing
                #region BranchOrder_Cancelation

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_Cancelation, eArg);

                #endregion




                ////INSERTING LOG OF ORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminCanclation, Order_ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);
            }
            return op;
        }

        public OperationResult AdminRollBackCancle(long Order_ID)
        {
            OperationResult op = new OperationResult();
            BranchOrder Order = Retrieve(Order_ID);

            Order.Status = (int)BranchOrderStatus.Pending;
            op = Update(Order);

            if (op.Result == ActionResult.Done && Order.BranchLackOrders.Count() > 0)
                for (int i = 0; i < 20; i++)
                {
                    op = new BranchLackOrderBusiness().AdminRollBackCancle(Order.BranchLackOrders.First().ID);
                    i = (op.Result == ActionResult.Done) ? 20 : i;
                }

            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                //op.AddMessage(UserMessageKey.BranchReturnOrderAdminCorrection);



                //Event Rasing
                #region BranchReturnOrder_RollBackCancle

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_RollBackCancle, eArg);

                #endregion



                ////INSERTING LOG OF ORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminRollingBackCanclation, Order_ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);

            }
            return op;
        }

        public OperationResult AdminEdit(BranchOrder Data)
        {
            long oldCost = new BranchOrderBusiness().Retrieve(Data.ID).TotalPrice;

            OperationResult op = new OperationResult();


            if (Data.Status != (int)BranchOrderStatus.Pending && Data.Status != (int)BranchOrderStatus.Canceled)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrder_PreventAdminEditCauseNotPending);
                return op;
            }


            OperationResult opSucceed = new OperationResult();

            if (Data.ID > 0)
            {
                opSucceed = base.Update(Data);
                  //Event Rasing
                #region BranchOrder_AdminEdit_BeforeConfirm

                SystemEventArgs eArg_edit = new SystemEventArgs();

                eArg_edit.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
                eArg_edit.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg_edit.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg_edit.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg_edit.EventVariables.Add("%id%", Data.ID.ToString());
                eArg_edit.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg_edit.EventVariables.Add("%oldcost%", UIUtil.GetCommaSeparatedOf(oldCost) + " ریال");
                eArg_edit.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg_edit.EventVariables.Add("%branchtitle%", eArg_edit.Related_Branch.Title);

                eArg_edit.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
                eArg_edit.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg_edit.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_EditedBeforeConfirmByCentralBranch, eArg_edit);

                #endregion
            }                
            else
            {
                opSucceed = base.Insert(Data);
                //Event Rasing
                #region BranchOrder_AdminEdit

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Data.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg.EventVariables.Add("%oldcost%", UIUtil.GetCommaSeparatedOf(oldCost) + " ریال");
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AdminEdit, eArg);

                #endregion
            }
               

            if (opSucceed.Result == ActionResult.Done)
            {
                opSucceed.Messages.Clear();
                opSucceed.AddMessage(UserMessageKey.BranchOrder_SucceedEditByAdmin);
            }


            ////INSERTING LOG OF ORDER CHANGE
            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminEditBeforConfirm, Data.ID, BranchFinanceLogType.BranchOrder);
            if (opLog.Result != ActionResult.Done)
                op.Messages.AddRange(opLog.Messages);

            return opSucceed;
        }

        public OperationResult AdminUpdateBeforeEdit(BranchOrder branchOrder)
        {

            if (branchOrder.ID > 0)
            {
                foreach (var item in branchOrder.BranchOrderDetails.ToList())
                    Context.BranchOrderDetails.DeleteObject(item);

                return Update(branchOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }

        public OperationResult AdminUpdateShipment(BranchOrder Data)
        {
            string oldState = "";
            BranchOrder oldOrder = new BranchOrderBusiness().Retrieve(Data.ID);
            if (oldOrder.ShipmentStatus_ID.HasValue)
                oldState = oldOrder.BranchOrderShipmentState.Title;

            OperationResult op = new OperationResult();

            op = Update(Data);

            if (op.Result == ActionResult.Done)
            {

                //Event Something
                #region BranchOrder_AdminUpdateShipment

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%oldstate%", oldState);
                eArg.EventVariables.Add("%state%", (Data.ShipmentStatus_ID.HasValue) ? Data.BranchOrderShipmentState.Title : "");
                eArg.EventVariables.Add("%id%", Data.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AdminUpdateShipment, eArg);

                #endregion



                ////INSERTING LOG OF ORDER CHANGE
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminShipmentUpdate, Data.ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);
            }

            return op;
        }

        public OperationResult AdminRollBackFinalized(long Order_ID)
        {
            OperationResult op = new OperationResult();
            BranchOrder Order = Retrieve(Order_ID);


            Order.Status = (int)BranchOrderStatus.Confirmed;
            op = Update(Order);


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchOrderAdminRollingBackFinalized);


                //Event Rasing
                #region BranchOrder_AdminRollBackFinalized

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AdminRollBackFinalized, eArg);

                #endregion



                //Inserting Log Of Rollback
                var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminRollingBackFinalized, Order_ID, BranchFinanceLogType.BranchOrder);
                if (opLog.Result != ActionResult.Done)
                    op.Messages.AddRange(opLog.Messages);


            }
            return op;
        }

        public OperationResult AdminDelete(long Order_ID)
        {
            OperationResult op = new OperationResult();
            BranchOrder Order = new BranchOrderBusiness().Retrieve(Order_ID);

            if (Order.BranchLackOrders.Count() > 0)
            {
                op = new BranchLackOrderBusiness().AdminDelete(Order.BranchLackOrders.First().ID);

                if (op.Result != ActionResult.Done)
                    return op;
            }

            op = new BranchTransactionBusiness().DeleteTransaction(BranchTransactionType.BranchOrder, Order_ID);

            if (op.Result != ActionResult.Done)
            {
                OperationResult opTransaction = new OperationResult();
                opTransaction.Result = op.Result;
                opTransaction.AddMessage(UserMessageKey.BranchTransactionDeleteFailed);
                return opTransaction;
            }


            var opLog = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAdminDelete, Order_ID, BranchFinanceLogType.BranchOrder);


            op = base.Delete(Order_ID);


            if (op.Result == ActionResult.Done)
            {
                op.Messages = new List<UserMessageKey>();
                op.AddMessage(UserMessageKey.BranchOrderAdminDelete);




                //Event Rasing
                #region BranchOrder_AdminDelete

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Order.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Order.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Order.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Order.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Order.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AdminDelete, eArg);

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

        public OperationResult AgentInsert(BranchOrder Data)
        {
            OperationResult op = new OperationResult();

            if (Data.Status != (int)BranchOrderStatus.Pending)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrder_PreventAdminEditCauseNotPending);
                return op;
            }


            bool isEdit = Data.ID > 0;

            OperationResult opSucceed = new OperationResult();

            if (Data.ID > 0)
            {
                opSucceed = base.Update(Data);
                //Event Rasing
                #region BranchOrder_Edit_BeforeConfirm

                SystemEventArgs eArg_Edit = new SystemEventArgs();

                eArg_Edit.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
                eArg_Edit.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg_Edit.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg_Edit.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg_Edit.EventVariables.Add("%id%", Data.ID.ToString());
                eArg_Edit.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg_Edit.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg_Edit.EventVariables.Add("%branchtitle%", eArg_Edit.Related_Branch.Title);

                eArg_Edit.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
                eArg_Edit.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg_Edit.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_EditedBeforeConfirm, eArg_Edit);

                #endregion
            }
            else
            {
                opSucceed = base.Insert(Data);
                //Event Rasing
                #region BranchOrder_Insert

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%id%", Data.ID.ToString());
                eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
                eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
                eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

                string productlist = "";

                foreach (var item in Data.BranchOrderDetails)
                {
                    string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                    productlist += temp;
                }
                if (productlist.Length > 1)
                    productlist = productlist.Substring(1);

                eArg.EventVariables.Add("%orderlist%", productlist);


                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_Insert, eArg);

                #endregion
            }

            if (opSucceed.Result == ActionResult.Done)
            {
                opSucceed.Messages.Clear();
                opSucceed.AddMessage(UserMessageKey.BranchOrderNew_SucceedByBranch);
            }

            ////INSERTING LOG OF ORDER CHANGE
            var opLog = BranchFinanceLogBusiness.InsertLog(
                                                            (isEdit) ?
                                                                BranchFinanceLogActionType.BranchOrderEditBeforConfirm
                                                                :
                                                                BranchFinanceLogActionType.BranchOrderAgentInserting,
                                                            Data.ID,
                                                            BranchFinanceLogType.BranchOrder);
            if (opLog.Result != ActionResult.Done)
                op.Messages.AddRange(opLog.Messages);

            return opSucceed;
        }

        public OperationResult AgentUpdateBeforeInsert(BranchOrder branchOrder)
        {
            if (branchOrder.ID > 0)
            {
                foreach (var item in branchOrder.BranchOrderDetails.ToList())
                    Context.BranchOrderDetails.DeleteObject(item);

                return Update(branchOrder);
            }

            return new OperationResult() { Result = ActionResult.Done };
        }

        public bool IsOpenForAgentAction(long Order_ID)
        {
            BranchOrder BranchOrder = Retrieve(Order_ID);

            string TimeForAcceptStart = OptionBusiness.GetText(OptionKey.BranchOrderNew_AcceptFrom);
            string TimeForAcceptEnd = OptionBusiness.GetText(OptionKey.BranchOrderNew_AcceptTo);
            string CurrentTime = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);


            return (BranchOrder.Status == (int)BranchOrderStatus.Pending &&
                    BranchOrder.OrderedPersianDate == DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1)) &&
                    CurrentTime.CompareTo(TimeForAcceptEnd) < 0 &&
                    CurrentTime.CompareTo(TimeForAcceptStart) > 0);
        }
        
        public override OperationResult Delete(long ID)
        {
            BranchOrder Data = new BranchOrderBusiness().Retrieve(ID);
            OperationResult op = BranchFinanceLogBusiness.InsertLog(BranchFinanceLogActionType.BranchOrderAgentDelete, ID, BranchFinanceLogType.BranchOrder);


            //Event Rasing
            #region BranchOrder_AgentDelete

            SystemEventArgs eArg = new SystemEventArgs();

            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);
            eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

            eArg.EventVariables.Add("%id%", Data.ID.ToString());
            eArg.EventVariables.Add("%deliverdate%", Data.OrderedPersianDate);
            eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.TotalPrice) + " ریال");
            eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

            eArg.EventVariables.Add("%pdwm%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), true));
            eArg.EventVariables.Add("%pdnom%", GetProductDetails(Data.BranchOrderDetails.AsQueryable(), false));

            string productlist = "";

            foreach (var item in Data.BranchOrderDetails)
            {
                string temp = string.Format(",{0}({1}عدد)", item.BranchOrderTitle_Title, item.Quantity);
                productlist += temp;
            }
            if (productlist.Length > 1)
                productlist = productlist.Substring(1);

            eArg.EventVariables.Add("%orderlist%", productlist);


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrder_AgentDelete, eArg);

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

        public long GetNumberOfStatus(BranchOrderStatus branchOrderStatus)
        {
            return Context.BranchOrders.Count(f => f.Status == (int)branchOrderStatus);
        }

        protected string GetProductDetails(IQueryable<BranchOrderDetail> list, bool withMoney)
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
