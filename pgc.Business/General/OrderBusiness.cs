using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.UI;

namespace pgc.Business.General
{
    public class OrderBusiness
    {
        pgcEntities db = new pgcEntities();

        public long AddNewOrder(Order model)
        {
            model.OrderDate = DateTime.Now;
            model.OrderPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
            model.OrderStatus = (int)OrderStatus.New;
            model.User_ID = UserSession.UserID;
            model.IsPaid = false;
            if (model.Branch_ID > 0)
                model.BranchTitle = db.Branches.FirstOrDefault(b => b.ID == model.Branch_ID).Title;
            else
            {
                model.BranchTitle = "";
                model.Branch_ID = null;
            }
            db.Orders.AddObject(model);
            db.SaveChanges();

            return model.ID;
        }


        public OperationResult AddOrderDetail(string itemsStr, long orderID)
        {
            OperationResult res = new OperationResult();
            try
            {
                string[] items = itemsStr.Split('|');
                foreach (var item in items)
                {
                    string[] p = item.Split(':');
                    OrderDetail order = new OrderDetail();
                    order.Order_ID = orderID;
                    order.Product_ID = ConvertorUtil.ToInt64(p[0]);
                    order.Quantity = ConvertorUtil.ToInt32(p[1]);
                    var product = db.Products.FirstOrDefault(d => d.ID == order.Product_ID);
                    order.UnitPrice = product.Price;
                    order.ProductTitle = product.Title;
                    order.SumPrice = order.Quantity * order.UnitPrice;
                    db.OrderDetails.AddObject(order);
                    db.SaveChanges();
                }
                res.AddMessage(UserMessageKey.Succeed);
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.ThankForOrder);
                return res;
            }
            catch (Exception)
            {
                res.AddMessage(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
            }

        }

        public List<Product> GetProducts()
        {
            return db.Products.Where(p => p.IsActive == true && p.AllowOnlineOrder == true).OrderBy(p => p.DispOrder).ToList();
        }

        public IQueryable BuyBasket_List()
        {
            //if (startRowIndex == 0 && maximumRows == 0)
            //    return null;

            var Result = BuyBasket_Where(db.BuyBaskets)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    product = f.Product.Title,
                    f.Quantity,
                    sum = f.Product.Price * f.Quantity
                });

            return Result;
            //return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int BuyBasket_Count()
        {
            return BuyBasket_Where(db.BuyBaskets).Count();
        }

        public IQueryable<BuyBasket> BuyBasket_Where(IQueryable<BuyBasket> list)
        {
            if (UserSession.IsUserLogined)
            {
                return list = db.BuyBaskets.Where(f => f.User_ID == UserSession.UserID);
            }
            else
            {
                return list;
            }
        }

        public Order RetriveOrder(long ID)
        {
            return db.Orders.Where(f => f.ID == ID).SingleOrDefault();
        }

        public IQueryable Order_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Order_Where(db.Orders)
                .OrderByDescending(f => f.OrderDate)
                .Select(f => new
                {
                    f.ID,
                    f.OrderDate,
                    f.OrderPersianDate,
                    f.PayableAmount,
                    f.PaymentType,
                    f.OrderStatus,
                    f.BranchTitle,
                    f.IsPaid
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Order_Count()
        {
            return Order_Where(db.Orders).Count();
        }

        public IQueryable<Order> Order_Where(IQueryable<Order> list)
        {
            if (UserSession.IsUserLogined)
            {
                return list = db.Orders.Where(f => f.User_ID == UserSession.UserID);
            }
            else
                return list;
        }

        public IQueryable OnlinePayment_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = OnlinePayment_where(db.OnlinePayments)
                .OrderByDescending(f => f.Date)
                .Select(f => new
                {
                    f.ID,
                    f.Order_ID,
                    f.ResNum,
                    f.RefNum,
                    f.Amount,
                    f.TransactionState,
                    f.Date,
                    f.ResultTransaction
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }
        public int OnlinePayment_Count()
        {
            return OnlinePayment_where(db.OnlinePayments).Count();
        }
        public IQueryable<OnlinePayment> OnlinePayment_where(IQueryable<OnlinePayment> list)
        {
            if (UserSession.IsUserLogined)
            {
                return list = db.OnlinePayments.Where(o => o.Order.User_ID == UserSession.UserID);
            }
            else
                return list;
        }

        public List<OrderDetail> GetOrderDetail(long OrderID)
        {
            return db.OrderDetails.Where(i => i.Order_ID == OrderID).ToList();
        }


        public long GetTotalPrice(BuyBasket buy)
        {
            long productsprice;

            if (db.BuyBaskets.Where(i => i.User_ID == UserSession.UserID).Count() > 0)
                productsprice = db.BuyBaskets.Where(i => i.User_ID == UserSession.UserID).Sum(i => i.Quantity * i.Product.Price);
            else
                productsprice = 0;

            return productsprice;
        }

        public long AddToBasket(long ProductID, int Quantity)
        {
            long TotalPrice = 0;
            //BuyBasket basket;
            List<BuyBasket> items = db.BuyBaskets.Where(b => b.User_ID == UserSession.UserID).ToList();
            if (items.Count != 0)
            {
                foreach (BuyBasket item in items)
                {
                    if (item.Product_ID == ProductID)
                    {
                        item.Quantity = item.Quantity + Quantity;
                        db.SaveChanges();
                        return TotalPrice = GetTotalPrice(item);
                    }
                }
                TotalPrice = AddNewBasket(ProductID, Quantity);
            }
            else
            {
                TotalPrice = AddNewBasket(ProductID, Quantity);
            }
            return TotalPrice;
        }

        private long AddNewBasket(long ProductID, int Quantity)
        {
            BuyBasket basket = new BuyBasket();
            long TotalPrice;
            basket.User_ID = UserSession.UserID;
            basket.Product_ID = ProductID;
            basket.Quantity = Quantity;
            db.BuyBaskets.AddObject(basket);
            db.SaveChanges();
            return TotalPrice = GetTotalPrice(basket);
        }

        public long DeleteOfBasket(long id)
        {
            BuyBasket basket;
            basket = db.BuyBaskets.Where(i => i.ID == id).SingleOrDefault();

            db.BuyBaskets.DeleteObject(basket);
            db.SaveChanges();

            long TotalPrice = GetTotalPrice(basket);
            return TotalPrice;
        }

        public OperationResult RegOrder(
            PaymentType payamentType,
            OrderStatus orderStatus,
            string address,
            string tel,
            string comment,
            long BranchID)
        {
            OperationResult res = new OperationResult();
            try
            {
                List<BuyBasket> items = db.BuyBaskets.Where(b => b.User_ID == UserSession.UserID).ToList();
                if (items.Count != 0)
                {
                    Order order = new Order();
                    foreach (BuyBasket item in items)
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.Product_ID = item.Product_ID;
                        detail.ProductTitle = db.Products.SingleOrDefault(f => f.ID == item.Product_ID).Title;
                        detail.Quantity = item.Quantity;
                        detail.UnitPrice = item.Product.Price;
                        detail.SumPrice = detail.Quantity * detail.UnitPrice;
                        order.OrderDetails.Add(detail);
                        db.BuyBaskets.DeleteObject(item);
                    }
                    order.Address = address;
                    order.Branch_ID = (BranchID == -1) ? null : (long?)BranchID;
                    if (order.Branch_ID.HasValue)
                    {
                        long branchID = order.Branch_ID.Value;
                        order.BranchTitle = db.Branches.SingleOrDefault(f => f.ID == branchID).Title;
                    }
                    else
                        order.BranchTitle = "";

                    order.Comment = comment;
                    order.OrderDate = DateTime.Now;
                    order.OrderPersianDate = DateUtil.GetPersianDateShortString(order.OrderDate);
                    order.OrderStatus = (int)orderStatus;
                    order.PaymentType = (int)payamentType;
                    order.Tel = tel;
                    order.TotalAmount = order.OrderDetails.Sum(o => o.SumPrice);
                    order.PayableAmount = order.TotalAmount;
                    order.User_ID = UserSession.UserID;

                    db.Orders.AddObject(order);
                    db.SaveChanges();
                    res.Result = ActionResult.Done;
                    res.AddMessage(UserMessageKey.Succeed);


                    //Order_New
                    #region Event Raising

                    SystemEventArgs e = new SystemEventArgs();
                    User user = new UserBusiness().RetriveUser(UserSession.UserID);

                    e.Related_User = user;
                    if (order.Branch_ID.HasValue)
                        e.Related_Branch = new BranchBusiness().RetirveBranchID(order.Branch_ID.Value);

                    //Details Of User
                    e.EventVariables.Add("%user%", e.Related_User.FullName);
                    e.EventVariables.Add("%username%", e.Related_User.Username);
                    e.EventVariables.Add("%email%", e.Related_User.Email);
                    e.EventVariables.Add("%mobile%", string.IsNullOrEmpty(e.Related_User.Mobile) ? e.Related_User.Tel : e.Related_User.Mobile);

                    //Details Of Order
                    e.EventVariables.Add("%phone%", order.Tel);
                    e.EventVariables.Add("%address%", string.IsNullOrEmpty(order.Address) ? e.Related_User.Address : order.Address);
                    e.EventVariables.Add("%comment%", order.Comment);
                    e.EventVariables.Add("%orderid%", order.ID.ToString());
                    e.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(order.TotalAmount) + " ریال");
                    e.EventVariables.Add("%branch%", order.Branch_ID.HasValue ? order.Branch.Title : "ندارد");
                    e.EventVariables.Add("%type%", ((PaymentType)order.PaymentType == PaymentType.Online) ? EnumUtil.GetEnumElementPersianTitle((PaymentType)order.PaymentType) + "(تا این لحظه عملیات پرداخت صورت نگرفته)" : EnumUtil.GetEnumElementPersianTitle((PaymentType)order.PaymentType));

                    string productlist = "";

                    foreach (var item in order.OrderDetails)
                    {
                        string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                        productlist += temp;
                    }
                    if (productlist.Length > 1)
                        productlist = productlist.Substring(1);

                    e.EventVariables.Add("%productlist%", productlist);


                    e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(order.OrderDate));
                    e.EventVariables.Add("%time%", order.OrderDate.TimeOfDay.ToString().Substring(0, 8));


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_New, e);

                    #endregion



                    return res;
                }
                else
                {

                    res.Result = ActionResult.Failed;
                    res.AddMessage(UserMessageKey.NoItemSelectedForOrder);
                    return res;
                }
            }
            catch
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }

        public List<Branch> GetBranchList()
        {
            string TimeNow = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
            return db.Branches.Where(P => P.AllowOnlineOrder && P.IsActive && P.AllowOnlineOrderTimeFrom.CompareTo(TimeNow) <= 0 && P.AllowOnlineOrderTimeTo.CompareTo(TimeNow) >= 0).OrderBy(p => p.DispOrder).ToList();


            //var list = from P in db.Branches
            //           where (P.AllowOnlineOrder && P.IsActive && P.AllowOnlineOrderTimeFrom.CompareTo(TimeNow) <= 0 && P.AllowOnlineOrderTimeTo.CompareTo(TimeNow) >= 0)
            //           orderby P.DispOrder
            //           select new
            //           {
            //               P.ID,
            //               P.Title
            //           };
            //return list;
        }

        //public OperationResult SaveOrder(Order order)
        //{
        //    IQueryable<BuyBasket> basket;


        //     OrderDetail detail = new OrderDetail();

        //    OperationResult res = new OperationResult();

        //    basket= db.BuyBaskets.Where(i => i.User_ID == UserSession.UserID);
        //    try
        //    {
        //         foreach (BuyBasket buy in basket)
        //         {
        //             detail.Quantity = buy.Quantity;
        //             detail.Product_ID = buy.Product_ID;
        //             detail.UnitPrice = buy.Product.Price;
        //             detail.SumPrice = detail.Quantity * detail.UnitPrice;
        //             db.OrderDetails.AddObject(detail);
        //             db.SaveChanges();
        //         }

        //        db.DeleteObject(basket);
        //        db.Orders.AddObject(order);
        //        db.SaveChanges();

        //        res.Result = ActionResult.Done;
        //        res.AddMessage(UserMessageKey.Succeed);
        //        res.AddMessage(UserMessageKey.RegisterGreeting);
        //        return res;
        //    }

        //    catch
        //    {
        //        res.Result = ActionResult.Failed;
        //        res.AddMessage(UserMessageKey.Failed);
        //        return res;
        //    }
        //}
    }
}
