using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;
using pgc.Business.General;

namespace pgc.Business
{
    public class OrdersAgentBusiness:BaseEntityManagementBusiness<Order,pgcEntities>
    {
        public OrdersAgentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, OrdersAgentPattern Pattern)
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
                    OnlinePayment = f.OnlinePayments.Where(g => g.ID == f.OnlinePayments.Max(t => t.ID))
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public IQueryable<Order> Search_Select(OrdersAgentPattern Pattern)
        {
            var Result = Search_Where(Context.Orders, Pattern)
                .OrderByDescending(f => f.OrderDate);            

            return Result;
        }


        public int Search_Count(OrdersAgentPattern Pattern)
        {
            return Search_Where(Context.Orders, Pattern).Count();
        }

        public IQueryable<Order> Search_Where(IQueryable<Order> list, OrdersAgentPattern Pattern)
        {
            list = list.Where(o => o.Branch_ID == UserSession.User.Branch_ID);

            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (Pattern.Numbers > 0)
                list = list.Where(o => o.User_ID == Pattern.Numbers || o.ID == Pattern.Numbers);


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
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.FirstNumber) <= 0);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.PayableAmount.CompareTo(Pattern.Amount.FirstNumber) == 0);
                    break;
            }

            return list;
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
                #region Order_Remove_Agent

                SystemEventArgs eArg = new SystemEventArgs();
                User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = user;
                eArg.Related_User = new pgc.Business.UserBusiness().Retrieve(order.User_ID);
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

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Remove_Agent, eArg);

                #endregion
            }

            return operation;
        }

        public override OperationResult Update(Order Data)
        {
            Order order = new OrderBusiness().RetriveOrder(Data.ID);
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {

                if (order.Branch_ID != Data.Branch_ID)
                {
                    //Order_Change_Branch
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

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Change_Branch, eArg);

                    #endregion
                }


                if (order.OrderStatus != Data.OrderStatus)
                {
                    //Order_Change_Status
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

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_Change_Status, eArg);
                    #endregion
                }
            }

            return op;
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
                    Product = f.Product.Title,
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
    }
}