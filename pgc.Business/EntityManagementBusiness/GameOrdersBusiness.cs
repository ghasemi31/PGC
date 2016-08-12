using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Business.General;
using kFrameWork.UI;
using System.Collections.Generic;
using pgc.Business.Core;

namespace pgc.Business
{
    public class GameOrdersBusiness : BaseEntityManagementBusiness<GameOrder, pgcEntities>
    {
        public GameOrdersBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, GameOrdersPattern Pattern)
        {
            try
            {
                if (startRowIndex == 0 && maximumRows == 0)
                    return null;

                var Result = Search_Where(Context.GameOrders, Pattern)
                    .OrderByDescending(f => f.OrderDate)
                    .Select(f => new
                    {
                        f.ID,
                        f.GameTitle,//.Substring(6),
                        //Name = f.User.Fname + " " + f.User.Lname,
                        f.OrderDate,
                        f.OrderPersianDate,
                        f.PayableAmount,
                        PayStatus = f.IsPaid ? "پرداخت شده" : "پرداخت نشده",
                        f.Name,
                        f.IsPaid,
                        OnlinePayment = f.Payments.Where(g => g.ID == f.Payments.Max(t => t.ID))
                    });

                return Result.Skip(startRowIndex).Take(maximumRows);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "GameOrderBusiness.Search_Select");
                return Search_Where(Context.GameOrders, Pattern).OrderByDescending(g => g.OrderDate).Skip(startRowIndex).Take(maximumRows);
            }
        }

        public IQueryable<GameOrder> Search_Select(GameOrdersPattern Pattern)
        {
            var Result = Search_Where(Context.GameOrders, Pattern)
                .OrderByDescending(f => f.OrderDate);

            return Result;
        }

        public int Search_Count(GameOrdersPattern Pattern)
        {
            return Search_Where(Context.GameOrders, Pattern).Count();
        }

        public IQueryable<GameOrder> Search_Where(IQueryable<GameOrder> list, GameOrdersPattern Pattern)
        {

            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(f => f.ID == Pattern.ID);

            //Search By Pattern
            if (Pattern.Numbers > 0)
                list = list.Where(o => o.User_ID == Pattern.Numbers || o.ID == Pattern.Numbers);

            if (Pattern.Game_ID > 0)
                list = list.Where(o => o.Game_ID == Pattern.Game_ID);


            if (!string.IsNullOrEmpty(Pattern.RefNum))
                list = list.Where(f => f.Payments.Any(h => h.RefNum.Contains(Pattern.RefNum)));

            if (!string.IsNullOrEmpty(Pattern.UserName))
                list = list.Where(f => f.User.Fname.Contains(Pattern.UserName) ||
                                    f.User.Lname.Contains(Pattern.UserName) ||
                                    f.User.Username.Contains(Pattern.UserName));
            try
            {
                if (BasePattern.IsEnumAssigned(Pattern.GameOrderPaymentStatus))
                    switch (Pattern.GameOrderPaymentStatus)
                    {
                        case GameOrderPaymentStatus.OnlineSucced:
                            list = list.Where(f => f.IsPaid);
                            break;
                        case GameOrderPaymentStatus.OnlineFailed:
                            list = list.Where(f => !f.IsPaid);
                            break;
                        default:
                            break;
                    }
            }
            catch (Exception Except)
            {
                ExceptionHandler.HandleManualException(Except, "GameOrderBusiness.Search_Where, SearchFor GameOrderPaymentStatus");
            }
            switch (Pattern.OrderPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.OrderPersianDate.HasFromDate && Pattern.OrderPersianDate.HasToDate)
                        list = list.Where(f => f.OrderPersianDate.CompareTo(Pattern.OrderPersianDate.FromDate) >= 0
                            && f.OrderPersianDate.CompareTo(Pattern.OrderPersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.OrderPersianDate.HasDate)
                        list = list.Where(f => f.OrderPersianDate.CompareTo(Pattern.OrderPersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.OrderPersianDate.HasDate)
                        list = list.Where(f => f.OrderPersianDate.CompareTo(Pattern.OrderPersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.OrderPersianDate.HasDate)
                        list = list.Where(f => f.OrderPersianDate.CompareTo(Pattern.OrderPersianDate.Date) == 0);
                    break;
            }


            switch (Pattern.Amount.Type)
            {
                case RangeType.Between:
                    if (Pattern.Amount.HasFirstNumber && Pattern.Amount.HasSecondNumber)
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.FirstNumber) >= 0
                            && f.PayableAmount.CompareTo(Pattern.Amount.SecondNumber) <= 0);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.FirstNumber) >= 0);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.SecondNumber) <= 0);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.FirstNumber) == 0);
                    break;
            }

            return list;
        }

        public override OperationResult Insert(pgc.Model.GameOrder Data)
        {
            return base.Insert(Data);
        }

        public override OperationResult Delete(long ID)
        {
            string stateOK = OnlineTransactionStatus.OK.ToString();
            if (Context.Payments.Any(f => f.Order_ID == ID))
            {
                OperationResult op = new OperationResult();
                op.AddMessage(UserMessageKey.PreventDeleteOrderForOnlinePayment);
                op.Result = ActionResult.Error;
                return op;
            }


            GameOrder GameOrder = Context.GameOrders.SingleOrDefault(f => f.ID == ID);
            OperationResult operation = base.Delete(ID);

            if (operation.Result == ActionResult.Done)
            {
                //BranchPage_Edti
                #region GameOrder_Remove_Admin

                //SystemEventArgs eArg = new SystemEventArgs();
                //User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                //eArg.Related_Doer = user;
                //eArg.Related_User = GameOrder.User;
                //if (GameOrder.Branch_ID.HasValue)
                //    eArg.Related_Branch = GameOrder.Branch;

                //eArg.EventVariables.Add("%user%", user.FullName);
                //eArg.EventVariables.Add("%username%", user.Username);
                //eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                //eArg.EventVariables.Add("%mobile%", user.Mobile);
                //eArg.EventVariables.Add("%email%", user.Email);


                //eArg.EventVariables.Add("%address%", GameOrder.Address);
                //eArg.EventVariables.Add("%GameOrderid%", GameOrder.ID.ToString());
                //eArg.EventVariables.Add("%comment%", GameOrder.Comment);
                //eArg.EventVariables.Add("%amount%", GameOrder.PayableAmount.ToString());

                //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.GameOrder_Remove_Admin, eArg);

                #endregion
            }



            return operation;
        }

        public override OperationResult Update(GameOrder Data)
        {
            OperationResult op = new OperationResult();
            GameOrder GameOrder = new GameOrderBusiness().RetriveGameOrder(Data.ID);
            #region Validation
            //// check wheter branch of paid GameOrder is changed or not
            //if (GameOrder.Branch_ID != Data.Branch_ID &&
            //    Data.Payments.Any(c => c.RefNum != "" && c.TransactionState == "OK"))
            //{
            //    // new branch should be selected!
            //    if (Data.Branch_ID == null)
            //    {
            //        op.Result = ActionResult.Failed;
            //        op.AddMessage(UserMessageKey.PaidGameOrderMustHaveBranch);
            //        return op;
            //    }
            //}
            #endregion
            op = base.Update(Data);

            //if (op.Result == ActionResult.Done)
            //{
            //    if (GameOrder.Branch_ID != Data.Branch_ID)
            //    {
            //        //update branchid in corresponding transaction records for Payments(if exists)
            //        OperationResult btop = UpdateBranchTransactions(GameOrder, Data);
            //        op.Messages.AddRange(btop.Messages);

            //        //GameOrder_Change_Branch_Admin
            //        #region Event Rising

            //        SystemEventArgs eArg = new SystemEventArgs();
            //        User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            //        eArg.Related_Doer = user;
            //        eArg.Related_User = new UserBusiness().Retrieve(Data.User_ID);
            //        if (Data.Branch_ID.HasValue)
            //            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

            //        eArg.EventVariables.Add("%user%", user.FullName);
            //        eArg.EventVariables.Add("%username%", user.Username);
            //        eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            //        eArg.EventVariables.Add("%mobile%", user.Mobile);
            //        eArg.EventVariables.Add("%email%", user.Email);


            //        eArg.EventVariables.Add("%address%", Data.Address);
            //        eArg.EventVariables.Add("%GameOrderid%", Data.ID.ToString());
            //        eArg.EventVariables.Add("%comment%", Data.Comment);
            //        eArg.EventVariables.Add("%amount%", Data.PayableAmount.ToString());

            //        EventHandlerBusiness.HandelSystemEvent(SystemEventKey.GameOrder_Change_Branch_Admin, eArg);

            //        #endregion
            //    }


            //    if (GameOrder.GameOrderStatus != Data.GameOrderStatus)
            //    {
            //        //GameOrder_Change_Status_Admin
            //        #region Event Rising

            //        SystemEventArgs eArg = new SystemEventArgs();
            //        User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            //        eArg.Related_Doer = user;
            //        eArg.Related_User = new UserBusiness().Retrieve(Data.User_ID);
            //        if (Data.Branch_ID.HasValue)
            //            eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

            //        eArg.EventVariables.Add("%user%", user.FullName);
            //        eArg.EventVariables.Add("%username%", user.Username);
            //        eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            //        eArg.EventVariables.Add("%mobile%", user.Mobile);
            //        eArg.EventVariables.Add("%email%", user.Email);

            //        eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((GameOrderStatus)GameOrder.GameOrderStatus));
            //        eArg.EventVariables.Add("%address%", Data.Address);
            //        eArg.EventVariables.Add("%GameOrderid%", Data.ID.ToString());
            //        eArg.EventVariables.Add("%comment%", Data.Comment);
            //        eArg.EventVariables.Add("%amount%", Data.PayableAmount.ToString());

            //        EventHandlerBusiness.HandelSystemEvent(SystemEventKey.GameOrder_Change_Status_Admin, eArg);
            //        #endregion
            //    }
            //}

            return op;
        }


       


        public IQueryable pay_List(int startRowIndex, int maximumRows, long ID)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var order = Context.GameOrders.SingleOrDefault(g => g.ID == ID);

            if (order == null )
                return null;

            var Result = order.Payments.AsQueryable()
                .OrderBy(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.PersianDate,
                    f.RefNum,
                    f.State,
                    f.BankGeway_Enum,
                    f.Date,
                    f.Amount

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }
    }
}