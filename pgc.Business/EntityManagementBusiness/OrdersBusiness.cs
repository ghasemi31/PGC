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
    public class OrdersBusiness : BaseEntityManagementBusiness<Order, pgcEntities>
    {
        public OrdersBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, OrdersPattern Pattern)
        {
            try
            {
                if (startRowIndex == 0 && maximumRows == 0)
                    return null;

                var Result = Search_Where(Context.Orders, Pattern)
                    .OrderByDescending(f => f.OrderDate)
                    .Select(f => new
                    {
                        f.ID,
                        BranchTitle = f.BranchTitle,//.Substring(6),
                        //Name = f.User.Fname + " " + f.User.Lname,
                        f.OrderDate,
                        f.OrderPersianDate,
                        f.PayableAmount,
                        f.PaymentType,
                        f.User.FullName,
                        f.DeviceType_Enum,
                        OnlinePayment = f.OnlinePayments.Where(g => g.ID == f.OnlinePayments.Max(t => t.ID))
                    });

                return Result.Skip(startRowIndex).Take(maximumRows);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "OrderBusiness.Search_Select");
                return Search_Where(Context.Orders, Pattern).OrderByDescending(g => g.OrderDate).Skip(startRowIndex).Take(maximumRows);
            }
        }

        public IQueryable<Order> Search_Select(OrdersPattern Pattern)
        {
            var Result = Search_Where(Context.Orders, Pattern)
                .OrderByDescending(f => f.OrderDate);

            return Result;
        }

        public int Search_Count(OrdersPattern Pattern)
        {
            return Search_Where(Context.Orders, Pattern).Count();
        }

        public IQueryable<Order> Search_Where(IQueryable<Order> list, OrdersPattern Pattern)
        {

            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(f => f.ID == Pattern.ID);

            //Search By Pattern
            if (Pattern.Numbers > 0)
                list = list.Where(o => o.User_ID == Pattern.Numbers || o.ID == Pattern.Numbers);

            if (Pattern.Branch_ID > 0)
                list = list.Where(o => o.Branch_ID == Pattern.Branch_ID);

            if (!string.IsNullOrEmpty(Pattern.BranchTitle))
                list = list.Where(f => f.BranchTitle.Contains(Pattern.BranchTitle));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(o => o.OrderStatus == (int)Pattern.Status);

            if (Pattern.Product_ID > 0)
                list = list.Where(o => o.OrderDetails.Any(d => d.Product_ID == Pattern.Product_ID));

            if (!string.IsNullOrEmpty(Pattern.RefNum))
                list = list.Where(f => f.OnlinePayments.Any(h => h.RefNum.Contains(Pattern.RefNum)));

            if (!string.IsNullOrEmpty(Pattern.UserName))
                list = list.Where(f => f.User.Fname.Contains(Pattern.UserName) ||
                                    f.User.Lname.Contains(Pattern.UserName) ||
                                    f.User.Username.Contains(Pattern.UserName));
            try
            {
                if (BasePattern.IsEnumAssigned(Pattern.OrderPaymentStatus))
                    switch (Pattern.OrderPaymentStatus)
                    {
                        case OrderPaymentStatus.Online:
                            list = list.Where(f => f.PaymentType == (int)PaymentType.Online);
                            break;
                        case OrderPaymentStatus.Presence:
                            list = list.Where(f => f.PaymentType == (int)PaymentType.Presence);
                            break;
                        case OrderPaymentStatus.OnlineSucced:
                            list = list.Where(f => f.OnlinePayments.Any(g => g.TransactionState == "OK" && g.ResultTransaction == f.PayableAmount && g.RefNum != ""));
                            break;
                        case OrderPaymentStatus.OnlineFailed:
                            list = list.Where(f => f.PaymentType == (int)PaymentType.Online && !f.OnlinePayments.Any(g => g.TransactionState == "OK" && g.ResultTransaction == f.PayableAmount && g.RefNum != ""));
                            break;
                        default:
                            break;
                    }
            }
            catch (Exception Except)
            {
                ExceptionHandler.HandleManualException(Except, "OrderBusiness.Search_Where, SearchFor OrderPaymentStatus");
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

        public override OperationResult Insert(pgc.Model.Order Data)
        {
            return base.Insert(Data);
        }

        public override OperationResult Delete(long ID)
        {
            string stateOK = OnlineTransactionStatus.OK.ToString();
            if (Context.OnlinePayments.Any(f => f.Order_ID == ID))
            {
                OperationResult op = new OperationResult();
                op.AddMessage(UserMessageKey.PreventDeleteOrderForOnlinePayment);
                op.Result = ActionResult.Error;
                return op;
            }


            Order order = Context.Orders.SingleOrDefault(f => f.ID == ID);
            OperationResult operation = base.Delete(ID);

            if (operation.Result == ActionResult.Done)
            {
                //BranchPage_Edti
                #region Order_Remove_Admin

                SystemEventArgs eArg = new SystemEventArgs();
                User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = user;
                eArg.Related_User = order.User;
                if (order.Branch_ID.HasValue)
                    eArg.Related_Branch = order.Branch;

                eArg.EventVariables.Add("%user%", user.FullName);
                eArg.EventVariables.Add("%username%", user.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", user.Mobile);
                eArg.EventVariables.Add("%email%", user.Email);


                eArg.EventVariables.Add("%address%", order.Address);
                eArg.EventVariables.Add("%orderid%", order.ID.ToString());
                eArg.EventVariables.Add("%comment%", order.Comment);
                eArg.EventVariables.Add("%amount%", order.PayableAmount.ToString());

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Remove_Admin, eArg);

                #endregion
            }



            return operation;
        }

        public override OperationResult Update(Order Data)
        {
            OperationResult op = new OperationResult();
            Order order = new OrderBusiness().RetriveOrder(Data.ID);
            #region Validation
            // check wheter branch of paid order is changed or not
            if (order.Branch_ID != Data.Branch_ID &&
                Data.OnlinePayments.Any(c => c.RefNum != "" && c.TransactionState == "OK"))
            {
                // new branch should be selected!
                if (Data.Branch_ID == null)
                {
                    op.Result = ActionResult.Failed;
                    op.AddMessage(UserMessageKey.PaidOrderMustHaveBranch);
                    return op;
                }
            }
            #endregion
            op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                if (order.Branch_ID != Data.Branch_ID)
                {
                    //update branchid in corresponding transaction records for onlinepayments(if exists)
                    OperationResult btop = UpdateBranchTransactions(order, Data);
                    op.Messages.AddRange(btop.Messages);

                    //Order_Change_Branch_Admin
                    #region Event Rising

                    SystemEventArgs eArg = new SystemEventArgs();
                    User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                    eArg.Related_Doer = user;
                    eArg.Related_User = new UserBusiness().Retrieve(Data.User_ID);
                    if (Data.Branch_ID.HasValue)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

                    eArg.EventVariables.Add("%user%", user.FullName);
                    eArg.EventVariables.Add("%username%", user.Username);
                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%mobile%", user.Mobile);
                    eArg.EventVariables.Add("%email%", user.Email);


                    eArg.EventVariables.Add("%address%", Data.Address);
                    eArg.EventVariables.Add("%orderid%", Data.ID.ToString());
                    eArg.EventVariables.Add("%comment%", Data.Comment);
                    eArg.EventVariables.Add("%amount%", Data.PayableAmount.ToString());

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Change_Branch_Admin, eArg);

                    #endregion
                }


                if (order.OrderStatus != Data.OrderStatus)
                {
                    //Order_Change_Status_Admin
                    #region Event Rising

                    SystemEventArgs eArg = new SystemEventArgs();
                    User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                    eArg.Related_Doer = user;
                    eArg.Related_User = new UserBusiness().Retrieve(Data.User_ID);
                    if (Data.Branch_ID.HasValue)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

                    eArg.EventVariables.Add("%user%", user.FullName);
                    eArg.EventVariables.Add("%username%", user.Username);
                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%mobile%", user.Mobile);
                    eArg.EventVariables.Add("%email%", user.Email);

                    eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((OrderStatus)order.OrderStatus));
                    eArg.EventVariables.Add("%address%", Data.Address);
                    eArg.EventVariables.Add("%orderid%", Data.ID.ToString());
                    eArg.EventVariables.Add("%comment%", Data.Comment);
                    eArg.EventVariables.Add("%amount%", Data.PayableAmount.ToString());

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Change_Status_Admin, eArg);
                    #endregion
                }
            }

            return op;
        }

        public OperationResult UpdateBranchTransactions(Order oldOrder, Order newOrder)
        {
            OperationResult res = new OperationResult();

            if (newOrder.OnlinePayments.Count > 0)
            {
                foreach (OnlinePayment payment in newOrder.OnlinePayments)
                {
                    if (payment.TransactionState == "OK" && payment.RefNum != "")
                    {
                        BranchTransaction transaction = Context.BranchTransactions.SingleOrDefault(bt => bt.TransactionType == 1 && bt.TransactionType_ID == payment.ID);
                        if (transaction != null)
                        {
                            transaction.Branch_ID = (long)newOrder.Branch_ID;
                            transaction.Description = OptionBusiness.GetText(OptionKey.BranchTransactionCustomerOnlineDesc).Replace("%name%", newOrder.Branch.Title);
                            Context.SaveChanges();
                            res.AddMessage(UserMessageKey.CorrespondingTransactionUpdated);
                        }
                    }
                }
                
            }
            res.Result = ActionResult.Done;
            return res;
        }

        public IQueryable OrderDetail_List(long OrderID)
        {
            //if (startRowIndex == 0 && maximumRows == 0)
            //    return null;

            var Result = OrderDetail_Where(Context.OrderDetails, OrderID)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.ProductTitle,
                    f.Quantity,
                    f.SumPrice
                });

            return Result;
            //return Result.Skip(startRowIndex).Take(maximumRows);

        }

        public int Order_Count(long OrderID)
        {
            return OrderDetail_Where(Context.OrderDetails, OrderID).Count();
        }

        public IQueryable<OrderDetail> OrderDetail_Where(IQueryable<OrderDetail> list, long OrderID)
        {

            return list = Context.OrderDetails.Where(f => f.Order_ID == OrderID);

        }

        public OrderPaymentStatus GetPaymentStatus(long ID)
        {
            try
            {
                Order order = Retrieve(ID);

                if (order.OnlinePayments.Any(g => g.TransactionState == "OK" && g.ResultTransaction == order.PayableAmount && g.RefNum != ""))
                    return OrderPaymentStatus.OnlineSucced;

                else if (order.PaymentType == (int)PaymentType.Presence)
                    return OrderPaymentStatus.Presence;

                else
                    return OrderPaymentStatus.OnlineFailed;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "OrderBusiness.GetPaymentStatus (OrderID=" + ID.ToString() + ")");
                return OrderPaymentStatus.Presence;
            }
        }
    }
}