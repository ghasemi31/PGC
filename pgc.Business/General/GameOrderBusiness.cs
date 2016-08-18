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
    public class GameOrderBusiness
    {
        pgcEntities db = new pgcEntities();

        public OperationResult AddNewOrder(GameOrder model)
        {


            OperationResult res = new OperationResult();


            try
            {
                model.OrderDate = DateTime.Now;
                model.OrderPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                model.User_ID = UserSession.UserID;
                var user = db.Users.FirstOrDefault(u => u.ID == model.User_ID);
                model.Name = user.FullName;
                model.Address = user.Address;
                model.Mobile = user.Mobile;
                model.Tel = user.Tel;

                model.IsPaid = false;
                model.GroupName = model.GroupName ?? "";
                var game = db.Games.FirstOrDefault(b => b.ID == model.Game_ID);
                model.GameTitle = game.Title;
                model.PayableAmount = game.Cost;

                if (user.GameCenter_ID != null)
                {
                    GameCenter center = new GameBusiness().RetriveGameCenter((long)user.GameCenter_ID);
                    model.GameCenter_ID = center.ID;
                    model.GameCenterTitle = center.TItle;
                }

                db.GameOrders.AddObject(model);
                db.SaveChanges();

                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);
                res.Data.Add("Order_ID", model.ID);


                //Order_New
                #region Event Raising

                //SystemEventArgs e = new SystemEventArgs();
                //User user = new pgc.Business.General.UserBusiness().RetriveUser(UserSession.UserID);

                //e.Related_User = user;
                //if (model.Branch_ID.HasValue)
                //    e.Related_Branch = new pgc.Business.General.BranchBusiness().RetirveBranchID(model.Branch_ID.Value);

                ////Details Of User
                //e.EventVariables.Add("%user%", e.Related_User.FullName);
                //e.EventVariables.Add("%username%", e.Related_User.Username);
                //e.EventVariables.Add("%email%", e.Related_User.Email);
                //e.EventVariables.Add("%mobile%", string.IsNullOrEmpty(e.Related_User.Mobile) ? e.Related_User.Tel : e.Related_User.Mobile);

                ////Details Of Order
                //e.EventVariables.Add("%phone%", model.Tel);
                //e.EventVariables.Add("%address%", string.IsNullOrEmpty(model.Address) ? e.Related_User.Address : model.Address);
                //e.EventVariables.Add("%comment%", model.Comment);
                //e.EventVariables.Add("%orderid%", model.ID.ToString());
                //e.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(model.TotalAmount) + " ریال");
                //e.EventVariables.Add("%branch%", model.Branch_ID.HasValue ? model.Branch.Title : "ندارد");
                //e.EventVariables.Add("%type%", ((PaymentType)model.PaymentType == PaymentType.Online) ? EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType) + "(تا این لحظه عملیات پرداخت صورت نگرفته)" : EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType));

                //string productlist = "";

                //foreach (var item in model.OrderDetails)
                //{
                //    string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
                //    productlist += temp;
                //}
                //if (productlist.Length > 1)
                //    productlist = productlist.Substring(1);

                //e.EventVariables.Add("%productlist%", productlist);


                //e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(model.OrderDate));
                //e.EventVariables.Add("%time%", model.OrderDate.TimeOfDay.ToString().Substring(0, 8));


                //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_New, e);

                #endregion

                return res;

            }
            catch(Exception e)
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }



        }



        public GameOrder RetriveGameOrder(long ID)
        {
            return db.GameOrders.Where(f => f.ID == ID).SingleOrDefault();
        }

        public IQueryable GameOrder_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = GameOrder_Where(db.GameOrders)
                .OrderByDescending(f => f.OrderDate)
                .Select(f => new
                {
                    f.ID,
                    f.OrderDate,
                    f.OrderPersianDate,
                    f.PayableAmount,
                    f.Name,
                    f.GameTitle,
                    f.GroupName,
                    f.IsPaid
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int GameOrder_Count()
        {
            return GameOrder_Where(db.GameOrders).Count();
        }

        public IQueryable<GameOrder> GameOrder_Where(IQueryable<GameOrder> list)
        {
            if (UserSession.IsUserLogined)
            {
                return list = db.GameOrders.Where(f => f.User_ID == UserSession.UserID);
            }
            else
                return list;
        }

        public IQueryable Payment_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Payment_where(db.Payments)
                .OrderByDescending(f => f.Date)
                .Select(f => new
                {
                    f.ID,
                    f.Order_ID,
                    f.BankGeway_Enum,
                    f.RefNum,
                    f.Amount,
                    f.IPAddress,
                    f.PersianDate,
                    f.Date
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }
        public int Payment_Count()
        {
            return Payment_where(db.Payments).Count();
        }
        public IQueryable<pgc.Model.Payment> Payment_where(IQueryable<pgc.Model.Payment> list)
        {
            if (UserSession.IsUserLogined)
            {
                return list = db.Payments.Where(o => o.GameOrder.User_ID == UserSession.UserID);
            }
            else
                return list;
        }






    }
}
